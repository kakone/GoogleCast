using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoogleCast.Channels;
using GoogleCast.Messages;
using GoogleCast.Models.Receiver;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf;

namespace GoogleCast;

/// <summary>
/// GoogleCast sender
/// </summary>
public class Sender : ISender
{
    private const int RECEIVE_TIMEOUT = 30000;

    /// <inheritdoc/>
    public event EventHandler? Disconnected;

    /// <summary>
    /// Initializes a new instance of <see cref="Sender"/> class
    /// </summary>
    public Sender() : this(new ServiceCollection().AddGoogleCast().BuildServiceProvider())
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="Sender"/> class
    /// </summary>
    /// <param name="serviceProvider">collection of service descriptors</param>
    public Sender(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
        var channels = serviceProvider.GetServices<IChannel>();
        Channels = channels;
        foreach (var channel in channels)
        {
            channel.Sender = this;
        }
    }

    private IServiceProvider ServiceProvider { get; }
    private IEnumerable<IChannel> Channels { get; set; }
    private IReceiver? Receiver { get; set; }
    private Stream? NetworkStream { get; set; }
    private TcpClient? TcpClient { get; set; }
    private SemaphoreSlim SendSemaphoreSlim { get; } = new SemaphoreSlim(1, 1);
    private SemaphoreSlim EnsureConnectionSemaphoreSlim { get; } = new SemaphoreSlim(1, 1);
    private ConcurrentDictionary<int, object> WaitingTasks { get; } = new ConcurrentDictionary<int, object>();
    private CancellationTokenSource? CancellationTokenSource { get; set; }

    /// <inheritdoc/>
    public void Disconnect()
    {
        foreach (var channel in GetStatusChannels())
        {
            channel.Status = null;
        }
        Dispose();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    private void Dispose()
    {
        if (TcpClient != null)
        {
            WaitingTasks.Clear();
            if (CancellationTokenSource != null)
            {
                CancellationTokenSource.Cancel();
                CancellationTokenSource = null;
            }
            Dispose(NetworkStream, () => NetworkStream = null);
            Dispose(TcpClient, () => TcpClient = null);
            OnDisconnected();
        }
    }

    private void Dispose(IDisposable? disposable, Action action)
    {
        if (disposable != null)
        {
            try
            {
                disposable.Dispose();
            }
            catch (Exception) { }
            finally
            {
                action();
            }
        }
    }

    /// <summary>
    /// Raises the Disconnected event
    /// </summary>
    protected virtual void OnDisconnected()
    {
        Disconnected?.Invoke(this, EventArgs.Empty);
    }

    /// <inheritdoc/>
    public TChannel GetChannel<TChannel>() where TChannel : IChannel
    {
        return Channels.OfType<TChannel>().FirstOrDefault();
    }

    /// <inheritdoc/>
    public async Task ConnectAsync(IReceiver receiver)
    {
        Dispose();

        Receiver = receiver;
        var tcpClient = new TcpClient();
        TcpClient = tcpClient;
        var ipEndPoint = receiver.IPEndPoint;
        var host = ipEndPoint.Address.ToString();
        await tcpClient.ConnectAsync(host, ipEndPoint.Port);
        var secureStream = new SslStream(tcpClient.GetStream(), true, (sender, certificate, chain, sslPolicyErrors) => true);
        await secureStream.AuthenticateAsClientAsync(host);
        NetworkStream = secureStream;

        Receive();
        await GetChannel<IConnectionChannel>().ConnectAsync();
    }

    private void Receive()
    {
        var cancellationTokenSource = new CancellationTokenSource();
        var cancellationToken = cancellationTokenSource.Token;
        CancellationTokenSource = cancellationTokenSource;
        Task.Run(async () =>
        {
            try
            {
                var channels = Channels;
                var messageTypes = ServiceProvider.GetRequiredService<IMessageTypes>();
                while (true)
                {
                    var buffer = await ReadAsync(4, cancellationToken);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(buffer);
                    }
                    var length = BitConverter.ToInt32(buffer, 0);
                    CastMessage castMessage;
                    using (var ms = new MemoryStream())
                    {
                        await ms.WriteAsync(await ReadAsync(length, cancellationToken), 0, length, cancellationToken);
                        ms.Position = 0;
                        castMessage = Serializer.Deserialize<CastMessage>(ms);
                    }
                    var payload = (castMessage.PayloadType == PayloadType.Binary ?
                        Encoding.UTF8.GetString(castMessage.PayloadBinary) : castMessage.PayloadUtf8)!;
                    Debug.WriteLine($"RECEIVED: {castMessage.Namespace} : {payload}");
                    var channel = channels.FirstOrDefault(c => c.Namespace == castMessage.Namespace);
                    if (channel != null)
                    {
                        var message = JsonSerializer.Deserialize<MessageWithId>(payload)!;
                        if (messageTypes.TryGetValue(message.Type, out var type))
                        {
                            try
                            {
                                var response = (IMessage)JsonSerializer.Deserialize(type, payload)!;
                                await channel.OnMessageReceivedAsync(response);
                                TaskCompletionSourceInvoke(message, "SetResult", response);
                            }
                            catch (Exception ex)
                            {
                                TaskCompletionSourceInvoke(message, "SetException", ex, new Type[] { typeof(Exception) });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                CancellationTokenSource = null;
                Dispose();
            }
        });
    }

    private void TaskCompletionSourceInvoke(MessageWithId message, string method, object parameter, Type[]? types = null)
    {
        if (message.HasRequestId && WaitingTasks.TryRemove(message.RequestId, out var tcs))
        {
            var tcsType = tcs.GetType();
            (types == null ? tcsType.GetMethod(method) : tcsType.GetMethod(method, types)).Invoke(tcs, new object[] { parameter });
        }
    }

    private async Task<byte[]> ReadAsync(int bufferLength, CancellationToken cancellationToken)
    {
        var buffer = new byte[bufferLength];
        int nb, length = 0;
        while (length < bufferLength)
        {
            nb = await NetworkStream!.ReadAsync(buffer, length, bufferLength - length, cancellationToken);
            if (nb == 0)
            {
                throw new InvalidOperationException();
            }
            length += nb;
        }
        return buffer;
    }

    private async Task EnsureConnectionAsync()
    {
        if (TcpClient == null && Receiver != null)
        {
            await EnsureConnectionSemaphoreSlim.WaitAsync();
            try
            {
                if (TcpClient == null && Receiver != null)
                {
                    await ConnectAsync(Receiver);
                }
            }
            finally
            {
                EnsureConnectionSemaphoreSlim.Release();
            }
        }
    }

    private async Task SendAsync(CastMessage castMessage)
    {
        await EnsureConnectionAsync();

        await SendSemaphoreSlim.WaitAsync();
        try
        {
            Debug.WriteLine($"SENT    : {castMessage.DestinationId}: {castMessage.PayloadUtf8}");

            byte[] message;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, castMessage);
                message = ms.ToArray();
            }
            var header = BitConverter.GetBytes(message.Length);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(header);
            }
            var networkStream = NetworkStream!;
            await networkStream.WriteAsync(header, 0, header.Length);
            await networkStream.WriteAsync(message, 0, message.Length);
            await networkStream.FlushAsync();
        }
        finally
        {
            SendSemaphoreSlim.Release();
        }
    }

    private CastMessage CreateCastMessage(string ns, string destinationId)
    {
        return new CastMessage()
        {
            Namespace = ns,
            SourceId = DefaultIdentifiers.SENDER_ID,
            DestinationId = destinationId
        };
    }

    /// <inheritdoc/>
    public async Task<ReceiverStatus> LaunchAsync<TAppChannel>() where TAppChannel : IApplicationChannel
    {
        return await LaunchAsync(GetChannel<TAppChannel>());
    }

    /// <inheritdoc/>
    public async Task<ReceiverStatus> LaunchAsync(IApplicationChannel applicationChannel)
    {
        return await LaunchAsync(applicationChannel.ApplicationId);
    }

    /// <inheritdoc/>
    public async Task<ReceiverStatus> LaunchAsync(string applicationId)
    {
        return await GetChannel<IReceiverChannel>().LaunchAsync(applicationId);
    }

    /// <inheritdoc/>
    public async Task SendAsync(string ns, IMessage message, string destinationId)
    {
        var castMessage = CreateCastMessage(ns, destinationId);
        castMessage.PayloadUtf8 = JsonSerializer.SerializeToUTF8String(message);
        await SendAsync(castMessage);
    }

    /// <inheritdoc/>
    public async Task<TResponse> SendAsync<TResponse>(string ns, IMessageWithId message, string destinationId)
        where TResponse : IMessageWithId
    {
        var taskCompletionSource = new TaskCompletionSource<TResponse>();
        WaitingTasks[message.RequestId] = taskCompletionSource;
        await SendAsync(ns, message, destinationId);
        return await taskCompletionSource.Task.TimeoutAfter(RECEIVE_TIMEOUT);
    }

    private IEnumerable<IStatusChannel> GetStatusChannels()
    {
        return Channels.OfType<IStatusChannel>();
    }

    /// <inheritdoc/>
    public IDictionary<string, object?> GetStatuses()
    {
        return GetStatusChannels().ToDictionary(c => c.Namespace, c => c.Status);
    }

    /// <inheritdoc/>
    public void RestoreStatuses(IDictionary<string, object> statuses)
    {
        var channels = GetStatusChannels();
        IStatusChannel channel;
        foreach (var keyValuePair in statuses)
        {
            channel = channels.FirstOrDefault(c => c.Namespace == keyValuePair.Key);
            if (channel != null)
            {
                channel.Status = keyValuePair.Value;
            }
        }
    }
}

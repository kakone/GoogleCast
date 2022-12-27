using System.Threading.Tasks;
using GoogleCast.Messages;

namespace GoogleCast.Channels;

/// <summary>
/// Channel base class
/// </summary>
public abstract class Channel : IChannel
{
    private const string BASE_NAMESPACE = "urn:x-cast:com.google.cast";

    /// <summary>
    /// Initialization
    /// </summary>
    protected Channel()
    {
    }

    /// <summary>
    /// Initialization
    /// </summary>
    /// <param name="ns">namespace</param>
    protected Channel(string ns)
    {
        Namespace = $"{BASE_NAMESPACE}.{ns}";
    }

    /// <inheritdoc/>
    public virtual ISender? Sender { get; set; }

    /// <inheritdoc/>
    public string Namespace { get; protected set; } = default!;

    /// <summary>
    /// Sends a message
    /// </summary>
    /// <param name="message">message to send</param>
    /// <param name="destinationId">destination identifier</param>
    protected async Task SendAsync(IMessage message, string destinationId = DefaultIdentifiers.DESTINATION_ID)
    {
        await Sender!.SendAsync(Namespace, message, destinationId);
    }

    /// <summary>
    /// Sends a message and waits the result
    /// </summary>
    /// <typeparam name="TResponse">response type</typeparam>
    /// <param name="message">message to send</param>
    /// <param name="destinationId">destination identifier</param>
    /// <returns>the result</returns>
    protected async Task<TResponse> SendAsync<TResponse>(IMessageWithId message, string destinationId = DefaultIdentifiers.DESTINATION_ID)
        where TResponse : IMessageWithId
    {
        return await Sender!.SendAsync<TResponse>(Namespace, message, destinationId);
    }

    /// <inheritdoc/>
    public virtual Task OnMessageReceivedAsync(IMessage message)
    {
        return Task.CompletedTask;
    }
}

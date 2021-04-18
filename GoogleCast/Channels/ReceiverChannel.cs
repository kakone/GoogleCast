using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleCast.Messages.Receiver;
using GoogleCast.Models;
using GoogleCast.Models.Receiver;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Receiver channel
    /// </summary>
    class ReceiverChannel : StatusChannel<ReceiverStatus, ReceiverStatusMessage>, IReceiverChannel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ReceiverChannel"/> class
        /// </summary>
        public ReceiverChannel() : base("receiver")
        {
        }

        /// <inheritdoc/>
        public override ISender? Sender
        {
            set
            {
                var sender = Sender;
                if (sender != value)
                {
                    if (sender != null)
                    {
                        sender.Disconnected -= Disconnected;
                    }
                    base.Sender = value;
                    if (value != null)
                    {
                        value.Disconnected += Disconnected;
                    }
                }
            }
        }

        private bool IsConnected { get; set; }

        /// <inheritdoc/>
        public async Task<ReceiverStatus> LaunchAsync(string applicationId)
        {
            return (await SendAsync<ReceiverStatusMessage>(new LaunchMessage() { ApplicationId = applicationId })).Status;
        }

        /// <inheritdoc/>
        public async Task<ReceiverStatus> SetVolumeAsync(float level)
        {
            return await SetVolumeAsync(level, null);
        }

        /// <inheritdoc/>
        public async Task<ReceiverStatus> SetIsMutedAsync(bool isMuted)
        {
            return await SetVolumeAsync(null, isMuted);
        }

        private async Task<ReceiverStatus> SetVolumeAsync(float? level, bool? isMuted)
        {
            return (await SendAsync<ReceiverStatusMessage>(
                new SetVolumeMessage()
                {
                    Volume = new Volume()
                    {
                        Level = level,
                        IsMuted = isMuted,
                    }
                })).Status;
        }

        private async Task<ReceiverStatus> CheckStatusAsync()
        {
            var status = Status;
            if (status == null)
            {
                status = await GetStatusAsync();
            }
            return status;
        }

        /// <inheritdoc/>
        public async Task<Application> EnsureConnectionAsync(string ns)
        {
            var status = await CheckStatusAsync();
            var application = status.Applications.First(a => a.Namespaces.Any(n => n.Name == ns));
            if (!IsConnected)
            {
                await Sender!.GetChannel<IConnectionChannel>().ConnectAsync(application.TransportId);
                IsConnected = true;
            }
            return application;
        }

        private void Disconnected(object sender, System.EventArgs e)
        {
            IsConnected = false;
        }

        /// <inheritdoc/>
        public async Task<ReceiverStatus?> StopAsync(params Application[] applications)
        {
            IEnumerable<Application> apps = applications;
            if (apps == null || !apps.Any())
            {
                apps = (await CheckStatusAsync()).Applications;
                if (apps == null || !apps.Any())
                {
                    return null;
                }
            }

            ReceiverStatusMessage? receiverStatusMessage = null;
            foreach (var application in apps)
            {
                receiverStatusMessage = await SendAsync<ReceiverStatusMessage>(new StopMessage() { SessionId = application.SessionId });
            }
            return receiverStatusMessage?.Status;
        }

        /// <inheritdoc/>
        public async Task<ReceiverStatus> GetStatusAsync()
        {
            return (await SendAsync<ReceiverStatusMessage>(new GetStatusMessage())).Status;
        }
    }
}

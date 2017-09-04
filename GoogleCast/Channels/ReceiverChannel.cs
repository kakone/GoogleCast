using GoogleCast.Messages.Receiver;
using GoogleCast.Models;
using GoogleCast.Models.Receiver;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Receiver channel
    /// </summary>
    class ReceiverChannel : StatusChannel<ReceiverStatusMessage, ReceiverStatus>, IReceiverChannel
    {
        /// <summary>
        /// Initializes a new instance of ReceiverChannel class
        /// </summary>
        public ReceiverChannel() : base("receiver")
        {
        }

        /// <summary>
        /// Gets or sets the sender
        /// </summary>
        public override ISender Sender
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

        /// <summary>
        /// Launches an application
        /// </summary>
        /// <param name="applicationId">application identifier</param>
        /// <returns>receiver status</returns>
        public async Task<ReceiverStatus> LaunchAsync(string applicationId)
        {
            return (await SendAsync<ReceiverStatusMessage>(new LaunchMessage() { ApplicationId = applicationId })).Status;
        }

        /// <summary>
        /// Sets the volume
        /// </summary>
        /// <param name="level">volume level (0.0 to 1.0)</param>
        /// <returns>receiver status</returns>
        public async Task<ReceiverStatus> SetVolumeAsync(float level)
        {
            return await SetVolumeAsync(level, null);
        }

        /// <summary>
        /// Sets a value indicating whether the audio should be muted
        /// </summary>
        /// <param name="isMuted">true if audio should be muted; otherwise, false</param>
        /// <returns>receiver status</returns>
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

        /// <summary>
        /// Checks the connection is well established
        /// </summary>
        /// <param name="ns">namespace</param>
        /// <returns>an application object</returns>
        public async Task<Application> EnsureConnection(string ns)
        {
            if (Status == null)
            {
                return null;
            }

            var application = Status.Applications.First(a => a.Namespaces.Any(n => n.Name == ns));
            if (!IsConnected)
            {
                await Sender.GetChannel<IConnectionChannel>().ConnectAsync(application.TransportId);
                IsConnected = true;
            }
            return application;
        }

        private void Disconnected(object sender, System.EventArgs e)
        {
            IsConnected = false;
        }
    }
}

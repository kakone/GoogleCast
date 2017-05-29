using GoogleCast.Messages;
using GoogleCast.Messages.Media;
using GoogleCast.Models.Media;
using GoogleCast.Models.Receiver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Media channel
    /// </summary>
    class MediaChannel : StatusChannel<MediaStatusMessage, IEnumerable<MediaStatus>>, IMediaChannel
    {
        /// <summary>
        /// Initializes a new instance of MediaChannel class
        /// </summary>
        public MediaChannel() : base("media")
        {
        }

        /// <summary>
        /// Gets the application identifier
        /// </summary>
        public string ApplicationId { get; } = "CC1AD845";

        private IReceiverChannel ReceiverChannel
        {
            get { return Sender.GetChannel<IReceiverChannel>(); }
        }

        private async Task<MediaStatus> SendAsync(IMessageWithId message, Application application)
        {
            application = (application ?? await ReceiverChannel.EnsureConnection(Namespace));
            try
            {
                return (await SendAsync<MediaStatusMessage>(message, application.TransportId)).Status?.FirstOrDefault();
            }
            catch (Exception)
            {
                Status = null;
                throw;
            }
        }

        private async Task<MediaStatus> SendAsync(MediaSessionMessage message)
        {
            message.MediaSessionId = Status?.First().MediaSessionId ?? throw new ArgumentNullException("MediaSessionId");
            return await SendAsync(message, null);
        }

        /// <summary>
        /// Loads a media
        /// </summary>
        /// <param name="media">media to load</param>
        /// <param name="autoPlay">true to play the media directly, false otherwise</param>
        /// <returns>media status</returns>
        public async Task<MediaStatus> LoadAsync(Media media, bool autoPlay = true)
        {
            var application = await ReceiverChannel.EnsureConnection(Namespace);
            return await SendAsync(new LoadMessage() { SessionId = application.SessionId, Media = media, AutoPlay = autoPlay }, application);
        }

        /// <summary>
        /// Plays the media
        /// </summary>
        /// <returns>media status</returns>
        public async Task<MediaStatus> PlayAsync()
        {
            return await SendAsync(new PlayMessage());
        }

        /// <summary>
        /// Pauses the media
        /// </summary>
        /// <returns>media status</returns>
        public async Task<MediaStatus> PauseAsync()
        {
            return await SendAsync(new PauseMessage());
        }

        /// <summary>
        /// Stops the media
        /// </summary>
        /// <returns>media status</returns>
        public async Task<MediaStatus> StopAsync()
        {
            return await SendAsync(new StopMessage());
        }

        /// <summary>
        /// Seeks to the specified time
        /// </summary>
        /// <param name="seconds">time in seconds</param>
        /// <returns>media status</returns>
        public async Task<MediaStatus> SeekAsync(double seconds)
        {
            return await SendAsync(new SeekMessage() { CurrentTime = seconds });
        }
    }
}

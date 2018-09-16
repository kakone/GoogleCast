using GoogleCast.Messages;
using GoogleCast.Messages.Media;
using GoogleCast.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Media channel
    /// </summary>
    class MediaChannel : StatusChannel<IEnumerable<MediaStatus>, MediaStatusMessage>, IMediaChannel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="MediaChannel"/> class
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

        private async Task<MediaStatus> SendAsync(IMessageWithId message)
        {
            var application = await ReceiverChannel.EnsureConnection(Namespace);
            switch (message)
            {
                case SessionMessage sessionMessage:
                    sessionMessage.SessionId = application.SessionId;
                    break;
                case MediaSessionMessage mediaSessionMessage:
                    SetMediaSessionId(mediaSessionMessage);
                    break;
            }

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

        private void SetMediaSessionId(MediaSessionMessage message)
        {
            message.MediaSessionId = Status?.First().MediaSessionId ?? throw new ArgumentNullException("MediaSessionId");
        }      

        /// <summary>
        /// Retrieves the media status
        /// </summary>
        /// <returns>the mediaa status</returns>
        public async Task<MediaStatus> GetStatusAsync()
        {
            return await SendAsync(new GetStatusMessage());
        }

        /// <summary>
        /// Loads a media
        /// </summary>
        /// <param name="media">media to load</param>
        /// <param name="autoPlay">true to play the media directly, false otherwise</param>
        /// <param name="activeTrackIds">track identifiers that should be active</param>
        /// <returns>media status</returns>
        public async Task<MediaStatus> LoadAsync(MediaInformation media, bool autoPlay = true, params int[] activeTrackIds)
        {
            return await SendAsync(new LoadMessage()
            {
                Media = media,
                AutoPlay = autoPlay,
                ActiveTrackIds = activeTrackIds
            });
        }

        /// <summary>
        /// Loads a queue items
        /// </summary>
        /// <param name="repeatMode">queue repeat mode</param>
        /// <param name="medias">media items</param>
        /// <returns>media status</returns>
        public Task<MediaStatus> QueueLoadAsync(RepeatMode repeatMode, params MediaInformation[] medias)
        {
            return QueueLoadAsync(repeatMode, medias.Select(mi => new QueueItem() { Media = mi }));
        }

        /// <summary>
        /// Loads a queue items
        /// </summary>
        /// <param name="repeatMode">queue repeat mode</param>
        /// <param name="queueItems">items to load</param>
        /// <returns>media status</returns>
        public Task<MediaStatus> QueueLoadAsync(RepeatMode repeatMode, params QueueItem[] queueItems)
        {
            return QueueLoadAsync(repeatMode, queueItems as IEnumerable<QueueItem>);
        }

        private async Task<MediaStatus> QueueLoadAsync(RepeatMode repeatMode, IEnumerable<QueueItem> queueItems)
        {
            return await SendAsync(new QueueLoadMessage()
            {
                RepeatMode = repeatMode,
                Items = queueItems
            });
        }

        /// <summary>
        /// Edits tracks info
        /// </summary>
        /// <param name="enabledTextTracks">true to enable text tracks, false otherwise</param>
        /// <param name="language">language for the tracks that should be active</param>
        /// <param name="activeTrackIds">track identifiers that should be active</param>
        /// <returns>media status</returns>
        public async Task<MediaStatus> EditTracksInfoAsync(string language = null, bool enabledTextTracks = true, params int[] activeTrackIds)
        {
            return await SendAsync(new EditTracksInfoMessage()
            {
                Language = language,
                EnableTextTracks = enabledTextTracks,
                ActiveTrackIds = activeTrackIds
            });
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

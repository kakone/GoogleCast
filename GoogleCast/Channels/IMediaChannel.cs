using GoogleCast.Models.Media;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for the media channel
    /// </summary>
    public interface IMediaChannel : IStatusChannel<IEnumerable<MediaStatus>>, IApplicationChannel
    {
        /// <summary>
        /// Loads a media
        /// </summary>
        /// <param name="media">media to load</param>
        /// <param name="autoPlay">true to play the media directly, false otherwise</param>
        /// <param name="activeTrackIds">tracks identifiers that should be active</param>
        /// <returns>media status</returns>
        Task<MediaStatus> LoadAsync(MediaInformation media, bool autoPlay = true, params int[] activeTrackIds);

        /// <summary>
        /// Loads a queue items
        /// </summary>
        /// <param name="repeatMode">queue repeat mode</param>
        /// <param name="medias">media items</param>
        /// <returns>media status</returns>
        Task<MediaStatus> QueueLoadAsync(RepeatMode repeatMode, params MediaInformation[] medias);

        /// <summary>
        /// Loads a queue items
        /// </summary>
        /// <param name="repeatMode">queue repeat mode</param>
        /// <param name="queueItems">items to load</param>
        /// <returns>media status</returns>
        Task<MediaStatus> QueueLoadAsync(RepeatMode repeatMode, params QueueItem[] queueItems);

        /// <summary>
        /// Edits tracks info
        /// </summary>
        /// <param name="enabledTextTracks">true to enable text tracks, false otherwise</param>
        /// <param name="language">language for the tracks that should be active</param>
        /// <param name="activeTrackIds">track identifiers that should be active</param>
        /// <returns>media status</returns>
        Task<MediaStatus> EditTracksInfoAsync(string language = null, bool enabledTextTracks = true, params int[] activeTrackIds);

        /// <summary>
        /// Plays the media
        /// </summary>
        /// <returns>media status</returns>
        Task<MediaStatus> PlayAsync();

        /// <summary>
        /// Pauses the media
        /// </summary>
        /// <returns>media status</returns>
        Task<MediaStatus> PauseAsync();

        /// <summary>
        /// Stops the media
        /// </summary>
        /// <returns>media status</returns>
        Task<MediaStatus> StopAsync();

        /// <summary>
        /// Seeks to the specified time
        /// </summary>
        /// <param name="seconds">time in seconds</param>
        /// <returns>media status</returns>
        Task<MediaStatus> SeekAsync(double seconds);
    }
}

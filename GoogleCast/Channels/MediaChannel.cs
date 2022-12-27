using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleCast.Messages;
using GoogleCast.Messages.Media;
using GoogleCast.Models.Media;
using GoogleCast.Models.Receiver;

namespace GoogleCast.Channels;

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

    /// <inheritdoc/>
    public string ApplicationId { get; } = "CC1AD845";

    private Task<Application> GetApplicationAsync()
    {
        return Sender!.GetChannel<IReceiverChannel>().EnsureConnectionAsync(Namespace);
    }

    private Task<MediaStatus?> SendAsync(MediaSessionMessage message, bool mediaSessionIdRequired = true)
    {
        var mediaSessionId = Status?.First().MediaSessionId;
        if (mediaSessionIdRequired && mediaSessionId == null)
        {
            throw new ArgumentNullException("MediaSessionId");
        }
        message.MediaSessionId = mediaSessionId;
        return SendAsync((IMessageWithId)message);
    }

    private async Task<MediaStatus?> SendAsync(IMessageWithId message)
    {
        try
        {
            return (await SendAsync<MediaStatusMessage>(message, (await GetApplicationAsync()).TransportId)).Status.FirstOrDefault();
        }
        catch (Exception)
        {
            ((IStatusChannel)this).Status = null;
            throw;
        }
    }

    /// <inheritdoc/>
    public Task<MediaStatus?> GetStatusAsync()
    {
        return SendAsync(new GetStatusMessage() { MediaSessionId = Status?.First().MediaSessionId }, false);
    }

    /// <inheritdoc/>
    public async Task<MediaStatus?> LoadAsync(MediaInformation media, bool autoPlay = true, params int[] activeTrackIds)
    {
        return await SendAsync(new LoadMessage()
        {
            Media = media,
            AutoPlay = autoPlay,
            ActiveTrackIds = activeTrackIds,
            SessionId = (await GetApplicationAsync()).SessionId
        });
    }

    /// <inheritdoc/>
    public Task<MediaStatus?> QueueLoadAsync(RepeatMode repeatMode, params MediaInformation[] medias)
    {
        return QueueLoadAsync(repeatMode, medias.Select(mi => new QueueItem() { Media = mi }));
    }

    /// <inheritdoc/>
    public Task<MediaStatus?> QueueLoadAsync(RepeatMode repeatMode, params QueueItem[] queueItems)
    {
        return QueueLoadAsync(repeatMode, queueItems as IEnumerable<QueueItem>);
    }

    private async Task<MediaStatus?> QueueLoadAsync(RepeatMode repeatMode, IEnumerable<QueueItem> queueItems)
    {
        return await SendAsync(new QueueLoadMessage()
        {
            RepeatMode = repeatMode,
            Items = queueItems
        });
    }

    /// <inheritdoc/>
    public async Task<MediaStatus?> EditTracksInfoAsync(string? language = null, bool enabledTextTracks = true, params int[] activeTrackIds)
    {
        return await SendAsync(new EditTracksInfoMessage()
        {
            Language = language,
            EnableTextTracks = enabledTextTracks,
            ActiveTrackIds = activeTrackIds
        });
    }

    /// <inheritdoc/>
    public async Task<MediaStatus?> PlayAsync()
    {
        return await SendAsync(new PlayMessage());
    }

    /// <inheritdoc/>
    public async Task<MediaStatus?> PauseAsync()
    {
        return await SendAsync(new PauseMessage());
    }

    /// <inheritdoc/>
    public async Task<MediaStatus?> StopAsync()
    {
        return await SendAsync(new StopMessage());
    }

    /// <inheritdoc/>
    public async Task<MediaStatus?> SeekAsync(double seconds)
    {
        return await SendAsync(new SeekMessage() { CurrentTime = seconds });
    }
}

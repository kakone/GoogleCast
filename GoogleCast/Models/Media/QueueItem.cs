using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Media;

/// <summary>
/// Queue item information
/// </summary>
[DataContract]
public class QueueItem
{
    /// <summary>
    /// Gets or sets the track identifiers that are active
    /// </summary>
    [DataMember(Name = "activeTrackIds", EmitDefaultValue = false)]
    public IEnumerable<int>? ActiveTrackIds { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the media will automatically play
    /// </summary>
    [DataMember(Name = "autoplay")]
    public bool Autoplay { get; set; } = true;

    /// <summary>
    /// Gets or sets the unique identifier of the item in the queue
    /// </summary>
    /// <remarks>the attribute is optional because for LOAD or INSERT should not be provided
    /// (as it will be assigned by the receiver when an item is first created/inserted).</remarks>
    [DataMember(Name = "itemId", EmitDefaultValue = false)]
    public int? ItemId { get; set; }

    /// <summary>
    /// Gets or sets the metadata (including contentId) of the playlist element
    /// </summary>
    [DataMember(Name = "media", EmitDefaultValue = false)]
    public MediaInformation? Media { get; set; }

    /// <summary>
    /// Gets or sets the seconds from the beginning of the media to start playback
    /// </summary>
    [DataMember(Name = "startTime", EmitDefaultValue = false)]
    public int? StartTime { get; set; }
}

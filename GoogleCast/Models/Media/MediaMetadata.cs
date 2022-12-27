using System.Runtime.Serialization;

namespace GoogleCast.Models.Media;

/// <summary>
/// Common media metadata
/// </summary>
[DataContract]
[KnownType(typeof(GenericMediaMetadata))]
[KnownType(typeof(MovieMetadata))]
[KnownType(typeof(MusicTrackMetadata))]
public abstract class MediaMetadata
{
    /// <summary>
    /// Gets the metadata type
    /// </summary>
    [DataMember(Name = "metadataType")]
    public abstract MetadataType MetadataType { get; }

    /// <summary>
    /// Gets or sets the image URL to be shown when video is loading
    /// </summary>
    [DataMember(Name = "title", EmitDefaultValue = false)]
    public string? PosterUrl { get; set; }
}

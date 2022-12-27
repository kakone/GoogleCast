using System.Runtime.Serialization;

namespace GoogleCast.Models.Media;

/// <summary>
/// Music track media description
/// </summary>
[DataContract]
public class MusicTrackMetadata : GenericMediaMetadata
{
    /// <inheritdoc/>
    public override MetadataType MetadataType => MetadataType.Music;

    /// <summary>
    /// Gets or sets the album artist name
    /// </summary>
    [DataMember(Name = "albumArtist", EmitDefaultValue = false)]
    public string? AlbumArtist { get; set; }

    /// <summary>
    /// Gets or sets the album name
    /// </summary>
    [DataMember(Name = "albumName", EmitDefaultValue = false)]
    public string? AlbumName { get; set; }

    /// <summary>
    /// Gets or sets the track artist name
    /// </summary>
    [DataMember(Name = "artist", EmitDefaultValue = false)]
    public string? Artist { get; set; }

    /// <summary>
    /// Gets or sets the tack composer name
    /// </summary>
    [DataMember(Name = "composer", EmitDefaultValue = false)]
    public string? Composer { get; set; }

    /// <summary>
    /// Gets or sets the disc number
    /// </summary>
    [DataMember(Name = "discNumber", EmitDefaultValue = false)]
    public int? DiscNumber { get; set; }

    /// <summary>
    /// Gets or sets the secondary image, for example, station logo for currently playing media
    /// </summary>
    [DataMember(Name = "secondaryImage", EmitDefaultValue = false)]
    public Image? SecondaryImage { get; set; }

    /// <summary>
    /// Gets or sets the track number in album
    /// </summary>
    [DataMember(Name = "trackNumber", EmitDefaultValue = false)]
    public int? TrackNumber { get; set; }
}

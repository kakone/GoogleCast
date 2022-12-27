using System.Runtime.Serialization;

namespace GoogleCast.Models.Media;

/// <summary>
/// Generic media description
/// </summary>
[DataContract]
public class GenericMediaMetadata : MediaMetadata
{
    /// <inheritdoc/>
    public override MetadataType MetadataType => MetadataType.Default;

    /// <summary>
    /// Gets or sets the content images, for example, cover art or a thumbnail of the currently-playing media
    /// </summary>
    [DataMember(Name = "images", EmitDefaultValue = false)]
    public Image[]? Images { get; set; }

    /// <summary>
    /// Gets or sets the date when the content was released, in ISO 8601 format, for example, 2014-02-10
    /// </summary>
    [DataMember(Name = "releaseDate", EmitDefaultValue = false)]
    public string? ReleaseDate { get; set; }

    /// <summary>
    /// Gets or sets the content subtitle
    /// </summary>
    [DataMember(Name = "subtitle", EmitDefaultValue = false)]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets the content title
    /// </summary>
    [DataMember(Name = "title", EmitDefaultValue = false)]
    public string? Title { get; set; }
}

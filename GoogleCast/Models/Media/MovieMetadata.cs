using System.Runtime.Serialization;

namespace GoogleCast.Models.Media;

/// <summary>
/// Movie media description
/// </summary>
[DataContract]
public class MovieMetadata : GenericMediaMetadata
{
    /// <inheritdoc/>
    public override MetadataType MetadataType => MetadataType.Movie;

    /// <summary>
    /// Gets or sets the studio
    /// </summary>
    [DataMember(Name = "studio", EmitDefaultValue = false)]
    public string? Studio { get; set; }
}

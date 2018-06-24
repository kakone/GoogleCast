using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Movie metadata
    /// </summary>
    [DataContract]
    public class MovieMetadata : GenericMediaMetadata
    {
        /// <summary>
        /// Gets the metadata type
        /// </summary>
        [DataMember(Name = "metadataType")]
        public override MetadataType MetadataType { get; } = MetadataType.Movie;

        /// <summary>
        /// Gets or sets the studio
        /// </summary>
        [DataMember(Name = "studio")]
        public string Studio { get; set; }
    }
}

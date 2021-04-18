using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Media metadata
    /// </summary>
    [DataContract]
    [KnownType(typeof(GenericMediaMetadata))]
    [KnownType(typeof(MovieMetadata))]
    public class GenericMediaMetadata
    {
        /// <summary>
        /// Initializes a new instance of <see cref="GenericMediaMetadata"/> class
        /// </summary>
        public GenericMediaMetadata()
        {
            MetadataType = MetadataType.Default;
        }

        /// <summary>
        /// Gets the metadata type
        /// </summary>
        [DataMember(Name = "metadataType")]
        public MetadataType MetadataType { get; protected set; }

        /// <summary>
        /// Gets or sets the descriptive title of the content
        /// </summary>
        [DataMember(Name = "title")]
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the descriptive subtitle of the content
        /// </summary>
        [DataMember(Name = "subtitle")]
        public string? Subtitle { get; set; }

        /// <summary>
        /// Gets or sets an array of URL(s) to an image associated with the content
        /// </summary>
        [DataMember(Name = "images")]
        public Image[]? Images { get; set; }
    }
}

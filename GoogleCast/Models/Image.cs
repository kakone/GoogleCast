using System.Runtime.Serialization;

namespace GoogleCast.Models
{
    /// <summary>
    /// Image
    /// </summary>
    [DataContract]
    public class Image
    {
        /// <summary>
        /// Gets or sets the URI for the image
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; } = default!;

        /// <summary>
        /// Gets or sets the height of the image
        /// </summary>
        [DataMember(Name = "height")]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the image
        /// </summary>
        [DataMember(Name = "width")]
        public int? Width { get; set; }
    }
}

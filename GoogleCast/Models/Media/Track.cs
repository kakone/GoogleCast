using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Track metadata information
    /// </summary>
    [DataContract]
    public class Track
    {
        /// <summary>
        /// Gets or sets the unique identifier of the track
        /// </summary>
        [DataMember(Name = "trackId")]
        public int TrackId { get; set; }

        /// <summary>
        /// Gets or sets the type of track
        /// </summary>
        [IgnoreDataMember]
        public TrackType Type { get; set; } = TrackType.Text;

        [DataMember(Name = "type")]
        private string TypeString
        {
            get { return Type.GetName(); }
            set { Type = EnumHelper.Parse<TrackType>(value); }
        }

        /// <summary>
        /// Gets or sets the MIME type of the track content
        /// </summary>
        [DataMember(Name = "trackContentType")]
        public string TrackContentType { get; set; } = "text/vtt";

        /// <summary>
        /// Gets or sets the identifier of the track’s content
        /// </summary>
        /// <remarks>it can be the url of the track or any other identifier that allows the receiver to find the content 
        /// (when the track is not inband or included in the manifest)</remarks>
        [DataMember(Name = "trackContentId")]
        public string TrackContentId { get; set; }

        /// <summary>
        /// Gets or sets the type of text track
        /// </summary>
        [IgnoreDataMember]
        public TextTrackType SubType { get; set; }

        [DataMember(Name = "subType")]
        private string SubTypeString
        {
            get { return SubType.GetName(); }
            set { SubType = EnumHelper.Parse<TextTrackType>(value); }
        }

        /// <summary>
        /// Gets or sets a descriptive, human readable name for the track
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the language tag as per RFC 5646
        /// </summary>
        /// <remarks>mandatory when the subtype is Subtitles</remarks>
        [DataMember(Name = "language")]
        public string Language { get; set; }
    }
}

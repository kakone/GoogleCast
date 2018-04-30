using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Media information
    /// </summary>
    [DataContract]
    public class MediaInformation
    {
        /// <summary>
        /// Gets or sets the content identifier
        /// </summary>
        [DataMember(Name = "contentId")]
        public string ContentId { get; set; }

        /// <summary>
        /// Gets or sets the stream type
        /// </summary>
        [IgnoreDataMember]
        public StreamType StreamType { get; set; } = StreamType.Buffered;

        [DataMember(Name = "streamType")]
        private string StreamTypeString
        {
            get { return StreamType.GetName(); }
            set { StreamType = EnumHelper.Parse<StreamType>(value); }
        }

        /// <summary>
        /// Gets or sets the content type
        /// </summary>
        [DataMember(Name = "contentType", EmitDefaultValue = false)]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the metadata
        /// </summary>
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public MediaMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets the duration of the media
        /// </summary>
        [DataMember(Name = "duration", EmitDefaultValue = false)]
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or sets the custom data
        /// </summary>
        [DataMember(Name = "customData", EmitDefaultValue = false)]
        public IDictionary<string, string> CustomData { get; set; }

        /// <summary>
        /// Gets or sets the tracks
        /// </summary>
        [DataMember(Name = "tracks")]
        public IEnumerable<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the style of text track
        /// </summary>
        [DataMember(Name = "textTrackStyle")]
        public TextTrackStyle TextTrackStyle { get; set; }
    }
}

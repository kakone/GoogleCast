using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Describes a media stream
    /// </summary>
    [DataContract]
    public class MediaInformation
    {
        /// <summary>
        /// Gets or sets the service-specific identifier of the content currently loaded by the media player
        /// </summary>
        [DataMember(Name = "contentId")]
        public string ContentId { get; set; }

        /// <summary>
        /// Gets or sets the type of media artifact
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
        /// Gets or sets the MIME content type of the media being played
        /// </summary>
        [DataMember(Name = "contentType", EmitDefaultValue = false)]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the media metadata object
        /// </summary>
        [DataMember(Name = "metadata", EmitDefaultValue = false)]
        public GenericMediaMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets the duration of the currently playing stream in seconds
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
        [DataMember(Name = "tracks", EmitDefaultValue = false)]
        public IEnumerable<Track> Tracks { get; set; }

        /// <summary>
        /// Gets or sets the style of text track
        /// </summary>
        [DataMember(Name = "textTrackStyle", EmitDefaultValue = false)]
        public TextTrackStyle TextTrackStyle { get; set; }
    }
}

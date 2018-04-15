using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Media
{
    /// <summary>
    /// Media item
    /// </summary>
    [DataContract]
    public class Media
    {
        /// <summary>
        /// Gets or sets the content identifier
        /// </summary>
        [DataMember(Name = "contentId")]
        public string ContentId { get; set; }

        /// <summary>
        /// Gets or sets the stream type
        /// </summary>
        public StreamType StreamType { get; set; } = StreamType.Buffered;

        [DataMember(Name = "streamType")]
        private string StreamTypeString
        {
            get { return Enum.GetName(typeof(StreamType), StreamType).ToUpperInvariant(); }
            set { StreamType = (StreamType)Enum.Parse(typeof(StreamType), value, true); }
        }

        /// <summary>
        /// Gets or sets the content type
        /// </summary>
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the metadata
        /// </summary>
        [DataMember(Name = "metadata")]
        public MediaMetadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets the duration of the media
        /// </summary>
        [DataMember(Name = "duration")]
        public double? Duration { get; set; }

        /// <summary>
        /// Gets or sets the custom data
        /// </summary>
        [DataMember(Name = "customData")]
        public IDictionary<string, string> CustomData { get; set; }

        /// <summary>
        /// Gets or sets the tracks
        /// </summary>
        [DataMember(Name = "tracks")]
        public IEnumerable<Track> Tracks { get; set; }
    }
}

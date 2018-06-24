using GoogleCast.Models.Media;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Message to load new content into the media player
    /// </summary>
    [DataContract]
    class LoadMessage : SessionMessage
    {
        /// <summary>
        /// Gets or sets the metadata (including contentId) of the media to load
        /// </summary>
        [DataMember(Name = "media")]
        public MediaInformation Media { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the media player will begin playing the content when it is loaded or not
        /// </summary>
        [DataMember(Name = "autoplay")]
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets the identifiers of the tracks that should be active. 
        /// </summary>
        /// <remarks>if the array is not provided, the default tracks will be active</remarks>
        [DataMember(Name = "activeTrackIds")]
        public IEnumerable<int> ActiveTrackIds { get; set; }
    }
}

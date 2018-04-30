using GoogleCast.Models.Media;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Load message
    /// </summary>
    [DataContract]
    class LoadMessage : SessionMessage
    {
        /// <summary>
        /// Gets or sets the media
        /// </summary>
        [DataMember(Name = "media")]
        public MediaInformation Media { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the media must be played directly or not
        /// </summary>
        [DataMember(Name = "autoplay")]
        public bool AutoPlay { get; set; }

        /// <summary>
        /// Gets or sets the identifiers of the tracks that should be active. 
        /// </summary>
        /// <remarks>If the array is not provided, the default tracks will be active</remarks>
        [DataMember(Name = "activeTrackIds")]
        public IEnumerable<int> ActiveTrackIds { get; set; }
    }
}

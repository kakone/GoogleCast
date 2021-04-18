using System.Collections.Generic;
using System.Runtime.Serialization;
using GoogleCast.Models.Media;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Media event EDIT_TRACKS_INFO request data
    /// </summary>
    [DataContract]
    class EditTracksInfoMessage : MediaSessionMessage
    {
        /// <summary>
        /// Gets or sets the track identifiers that should be active
        /// </summary>
        [DataMember(Name = "activeTrackIds")]
        public IEnumerable<int>? ActiveTrackIds { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the text tracks should be enabled or not
        /// </summary>
        [DataMember(Name = "enableTextTracks")]
        public bool EnableTextTracks { get; set; }

        /// <summary>
        /// Gets or sets the language for the tracks that should be active
        /// </summary>
        [DataMember(Name = "language")]
        public string? Language { get; set; }

        /// <summary>
        /// Gets or sets the text track style.
        /// If it is not provided the existing style will be used (if no style was provided in previous calls, it will be the default receiver style)
        /// </summary>
        [DataMember(Name = "textTrackStyle")]
        public TextTrackStyle? TextTrackStyle { get; set; }
    }
}

using System.Runtime.Serialization;

namespace GoogleCast.Models.Cast
{
    /// <summary>
    /// Media status
    /// </summary>
    [DataContract]
    public class CastStatus
    {
        /// <summary>
        /// Gets or sets the playback rate
        /// </summary>
        [DataMember(Name = "url")]
        public string? Url { get; set; }
    }
}

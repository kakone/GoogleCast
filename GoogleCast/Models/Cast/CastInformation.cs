using System.Runtime.Serialization;

namespace GoogleCast.Models.Cast
{
    /// <summary>
    /// Cast Information
    /// </summary>
    [DataContract]
    public class CastInformation
    {
        /// <summary>
        /// Gets or sets the URL to display
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}

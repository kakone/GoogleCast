using System.Runtime.Serialization;

namespace GoogleCast.Messages
{
    /// <summary>
    /// Session message base class
    /// </summary>
    [DataContract]
    public abstract class SessionMessage : MessageWithId
    {
        /// <summary>
        /// Gets or sets the session identifier
        /// </summary>
        [DataMember(Name = "sessionId")]
        public string SessionId { get; set; }
    }
}

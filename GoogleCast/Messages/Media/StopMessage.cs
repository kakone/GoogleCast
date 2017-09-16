using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Stop message
    /// </summary>
    [DataContract]
    class StopMessage : MediaSessionMessage
    {
        /// <summary>
        /// Gets or sets the receiver session identifier
        /// </summary>
        [DataMember(Name = "sessionId", EmitDefaultValue = false)]
        public string SessionId { get; set; }
    }
}

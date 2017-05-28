using System.Runtime.Serialization;

namespace GoogleCast.Messages
{
    /// <summary>
    /// Message base class
    /// </summary>
    [DataContract]
    public abstract class Message : IMessage
    {
        /// <summary>
        /// Initialization
        /// </summary>
        public Message()
        {
            var type = GetType().Name;
            Type = type.Substring(0, type.LastIndexOf(nameof(Message))).ToUpper();
        }

        /// <summary>
        /// Gets or sets the message type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}
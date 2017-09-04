using GoogleCast.Models;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Receiver
{
    /// <summary>
    /// Volume Message
    /// </summary>
    [DataContract]
    class SetVolumeMessage : MessageWithId
    {
        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        [DataMember(Name = "volume")]
        public Volume Volume { get; set; }
    }
}

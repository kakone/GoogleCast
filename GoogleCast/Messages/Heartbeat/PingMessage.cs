using System.Runtime.Serialization;

namespace GoogleCast.Messages.Heartbeat
{
    /// <summary>
    /// Ping message
    /// </summary>
    [DataContract]
    class PingMessage : Message
    {
    }
}

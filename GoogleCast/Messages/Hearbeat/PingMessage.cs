using System.Runtime.Serialization;

namespace GoogleCast.Messages.Heartbeat
{
    /// <summary>
    /// Ping message
    /// </summary>
    [DataContract]
    [ReceptionMessage]
    class PingMessage : Message
    {
    }
}

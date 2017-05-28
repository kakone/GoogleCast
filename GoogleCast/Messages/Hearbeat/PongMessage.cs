using System.Runtime.Serialization;

namespace GoogleCast.Messages.Heartbeat
{
    /// <summary>
    /// Pong message
    /// </summary>
    [DataContract]
    class PongMessage : Message
    {
    }
}

using System.Runtime.Serialization;

namespace GoogleCast.Messages.Connection
{
    /// <summary>
    /// Close message
    /// </summary>
    [DataContract]
    [ReceptionMessage]
    class CloseMessage : Message
    {
    }
}

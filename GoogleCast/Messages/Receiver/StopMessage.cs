using System.Runtime.Serialization;

namespace GoogleCast.Messages.Receiver;

/// <summary>
/// Stop message
/// </summary>
[DataContract]
class StopMessage : SessionMessage
{
}

using System.Runtime.Serialization;

namespace GoogleCast.Messages.Receiver;

/// <summary>
/// Get status message
/// </summary>
[DataContract]
class GetStatusMessage : MessageWithId
{
}

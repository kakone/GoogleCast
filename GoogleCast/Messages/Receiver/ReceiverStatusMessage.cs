using GoogleCast.Models.Receiver;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Receiver;

/// <summary>
/// Receiver status message
/// </summary>
[DataContract]
[ReceptionMessage]
class ReceiverStatusMessage : StatusMessage<ReceiverStatus>
{
}

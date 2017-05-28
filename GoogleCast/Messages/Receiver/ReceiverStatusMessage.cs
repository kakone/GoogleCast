using GoogleCast.Models.Receiver;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Receiver
{
    /// <summary>
    /// Receiver status message
    /// </summary>
    [DataContract]
    class ReceiverStatusMessage : StatusMessage<ReceiverStatus>
    {
    }
}

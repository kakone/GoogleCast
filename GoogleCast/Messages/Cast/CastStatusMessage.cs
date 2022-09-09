using GoogleCast.Models.Cast;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Cast
{
    /// <summary>
    /// Message to retrieve the cast status
    /// </summary>
    [DataContract]
    [ReceptionMessage]
    class CastStatusMessage : StatusMessage<IEnumerable<CastStatus>>
    {
    }
}

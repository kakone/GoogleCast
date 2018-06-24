using GoogleCast.Models.Media;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Message to retrieve the media status
    /// </summary>
    [DataContract]
    [ReceptionMessage]
    class MediaStatusMessage : StatusMessage<IEnumerable<MediaStatus>>
    {
    }
}

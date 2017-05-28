using GoogleCast.Models.Media;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Media status message
    /// </summary>
    [DataContract]
    class MediaStatusMessage : StatusMessage<IEnumerable<MediaStatus>>
    {
    }
}

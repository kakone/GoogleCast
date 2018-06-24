using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Message to stop playback of the current content
    /// </summary>
    [DataContract]
    class StopMessage : MediaSessionMessage
    {
    }
}

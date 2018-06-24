using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media
{
    /// <summary>
    /// Message to pause playback of the current content
    /// </summary>
    [DataContract]
    class PauseMessage : MediaSessionMessage
    {
    }
}

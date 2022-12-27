using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media;

/// <summary>
/// Message to begin playback of the content that was loaded with the load call
/// </summary>
[DataContract]
class PlayMessage : MediaSessionMessage
{
}

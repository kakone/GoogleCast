using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media;

/// <summary>
/// Message to retrieve the media status
/// </summary>
[DataContract]
class GetStatusMessage : MediaSessionMessage
{
}

using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media;

/// <summary>
/// Media session message
/// </summary>
[DataContract]
abstract class MediaSessionMessage : MessageWithId
{
    /// <summary>
    /// Gets or sets the media session identifier
    /// </summary>
    [DataMember(Name = "mediaSessionId", EmitDefaultValue = false)]
    public long? MediaSessionId { get; set; }
}

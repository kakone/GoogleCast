using System.Runtime.Serialization;
using GoogleCast.Models;

namespace GoogleCast.Messages.Receiver;

/// <summary>
/// Volume Message
/// </summary>
[DataContract]
class SetVolumeMessage : MessageWithId
{
    /// <summary>
    /// Gets or sets the volume
    /// </summary>
    [DataMember(Name = "volume")]
    public Volume Volume { get; set; } = default!;
}

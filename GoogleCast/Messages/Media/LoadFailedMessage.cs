using System;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media;

/// <summary>
/// Load failed message
/// </summary>
[DataContract]
[ReceptionMessage]
class LoadFailedMessage : MessageWithId
{
    [OnDeserializing]
    private void OnDeserializing(StreamingContext context)
    {
        throw new Exception("Load failed");
    }
}

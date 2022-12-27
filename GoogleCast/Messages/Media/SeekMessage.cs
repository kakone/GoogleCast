using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media;

/// <summary>
/// Message to set the current position in the stream
/// </summary>
[DataContract]
class SeekMessage : MediaSessionMessage
{
    /// <summary>
    /// Gets or sets the seconds since beginning of content
    /// </summary>
    /// <remarks>if the content is live content, and position is not specifed, the stream will start at the live position</remarks>
    [DataMember(Name = "currentTime")]
    public double CurrentTime { get; set; }
}

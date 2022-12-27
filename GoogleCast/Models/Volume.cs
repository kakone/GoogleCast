using System.Runtime.Serialization;

namespace GoogleCast.Models;

/// <summary>
/// Volume
/// </summary>
[DataContract]
public class Volume
{
    /// <summary>
    /// Gets or sets the volume level
    /// </summary>
    [DataMember(Name = "level", EmitDefaultValue = false)]
    public float? Level { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the audio is muted
    /// </summary>
    [DataMember(Name = "muted", EmitDefaultValue = false)]
    public bool? IsMuted { get; set; }

    /// <summary>
    /// Gets or sets the step interval
    /// </summary>
    [DataMember(Name = "stepInterval")]
    public float StepInterval { get; set; }
}
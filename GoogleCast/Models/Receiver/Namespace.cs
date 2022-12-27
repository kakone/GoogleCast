using System.Runtime.Serialization;

namespace GoogleCast.Models.Receiver;

/// <summary>
/// Namespace
/// </summary>
[DataContract]
public class Namespace
{
    /// <summary>
    /// Gets or sets the name
    /// </summary>
    [DataMember(Name = "name")]
    public string Name { get; set; } = default!;
}

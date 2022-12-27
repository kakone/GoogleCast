using ProtoBuf;

namespace GoogleCast;

/// <summary>
/// Cast message
/// </summary>
[ProtoContract]
class CastMessage
{
    /// <summary>
    /// Gets or sets the protocol version
    /// </summary>
    [ProtoMember(1, IsRequired = true, Name = "protocol_version")]
    public ProtocolVersion ProtocolVersion { get; set; }

    /// <summary>
    /// Gets or sets the source identifier
    /// </summary>
    [ProtoMember(2, IsRequired = true, Name = "source_id")]
    public string SourceId { get; set; } = "sender-0";

    /// <summary>
    /// Gets or sets the destination identifier
    /// </summary>
    [ProtoMember(3, IsRequired = true, Name = "destination_id")]
    public string DestinationId { get; set; } = "receiver-0";

    /// <summary>
    /// Gets or sets the namespace
    /// </summary>
    [ProtoMember(4, IsRequired = true, Name = "namespace")]
    public string Namespace { get; set; } = default!;

    /// <summary>
    /// Gets or sets the payload type
    /// </summary>
    [ProtoMember(5, IsRequired = true, Name = "payload_type")]
    public PayloadType PayloadType { get; set; } = default!;

    /// <summary>
    /// Gets or sets the UTF-8 payload
    /// </summary>
    [ProtoMember(6, IsRequired = false, Name = "payload_utf8")]
    public string? PayloadUtf8 { get; set; }

    /// <summary>
    /// Gets or sets the binary payload
    /// </summary>
    [ProtoMember(7, IsRequired = false, Name = "payload_binary")]
    public byte[]? PayloadBinary { get; set; }
}

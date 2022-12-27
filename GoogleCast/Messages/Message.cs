using System;
using System.Runtime.Serialization;

namespace GoogleCast.Messages;

/// <summary>
/// Message base class
/// </summary>
[DataContract]
public abstract class Message : IMessage
{
    /// <summary>
    /// Initialization
    /// </summary>
    public Message()
    {
        Type = GetMessageType(GetType());
    }

    /// <summary>
    /// Gets or sets the message type
    /// </summary>
    [DataMember(Name = "type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets the message type
    /// </summary>
    /// <returns>message class type</returns>
    public static string GetMessageType(Type type)
    {
        var typeName = type.Name;
        return typeName.Substring(0, typeName.LastIndexOf(nameof(Message))).ToUnderscoreUpperInvariant();
    }
}
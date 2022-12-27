﻿using System;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Media;

/// <summary>
/// Invalid request message
/// </summary>
[DataContract]
[ReceptionMessage]
class InvalidRequestMessage : MessageWithId
{
    /// <summary>
    /// Gets or sets the reason
    /// </summary>
    [DataMember(Name = "reason")]
    public string Reason { get; set; } = default!;

    [OnDeserialized]
    private void OnDeserialized(StreamingContext context)
    {
        throw new InvalidOperationException(Reason);
    }
}

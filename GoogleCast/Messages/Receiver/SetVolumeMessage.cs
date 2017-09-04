using System;
using System.Runtime.Serialization;

namespace GoogleCast.Messages.Receiver
{
	/// <summary>
	/// Set Volume Message
	/// </summary>
	[DataContract]
	class SetVolumeMessage : MessageWithId
	{
		[DataMember(Name = "volume")]
		public Models.Volume Volume { get; set; }
    }
}

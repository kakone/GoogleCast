using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Receiver
{
    /// <summary>
    /// Receiver status
    /// </summary>
    [DataContract]
    public class ReceiverStatus
    {
        /// <summary>
        /// Gets or sets the applications collection
        /// </summary>
        [DataMember(Name = "applications")]
        public IEnumerable<Application> Applications { get; set; } = default!;

        /// <summary>
        /// Gets or sets the volume
        /// </summary>
        [DataMember(Name = "volume")]
        public Volume Volume { get; set; } = default!;
    }
}

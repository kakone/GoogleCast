using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using GoogleCast.Models.Cast;

namespace GoogleCast.Messages.Cast
{
    /// <summary>
    /// Message to load specific URL
    /// </summary>
    [DataContract]
    [ReceptionMessage]
    class CastLoadMessage : SessionMessage
    {
        /// <summary>
        /// Gets or sets the url to load
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; } = default!;

        /// <summary>
        /// Gets or sets force information
        /// </summary>
        [DataMember(Name = "force")]
        public bool Force { get; set; } = false;

        /// <summary>
        /// Gets or sets reload information
        /// </summary>
        [DataMember(Name = "reload")]
        public bool Reload { get; set; } = false;

        /// <summary>
        /// Gets or sets reload time
        /// </summary>
        [DataMember(Name = "reload_time")]
        public int ReloadTime { get; set; } = 0;
    }
}

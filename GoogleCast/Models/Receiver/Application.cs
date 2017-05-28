using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GoogleCast.Models.Receiver
{
    /// <summary>
    /// Application
    /// </summary>
    [DataContract]
    public class Application
    {
        /// <summary>
        /// Gets or sets the application identifier
        /// </summary>
        [DataMember(Name = "appId")]
        public string AppId { get; set; }

        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        [DataMember(Name = "displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the backdrop app is running or not 
        /// </summary>
        [DataMember(Name = "isIdleScreen")]
        public bool IsIdleScreen { get; set; }

        /// <summary>
        /// Gets or sets the namespaces
        /// </summary>
        [DataMember(Name = "namespaces")]
        public IEnumerable<Namespace> Namespaces { get; set; }

        /// <summary>
        /// Gets or sets the session identifier
        /// </summary>
        [DataMember(Name = "sessionId")]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the status text
        /// </summary>
        [DataMember(Name = "statusText")]
        public string StatusText { get; set; }

        /// <summary>
        /// Gets or sets the transport identifier
        /// </summary>
        [DataMember(Name = "transportId")]
        public string TransportId { get; set; }
    }
}

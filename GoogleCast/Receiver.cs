using System.Net;

namespace GoogleCast
{
    /// <summary>
    /// GoogleCast receiver
    /// </summary>
    public class Receiver : IReceiver
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Gets or sets the friendly name
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the network endpoint
        /// </summary>
        public IPEndPoint IPEndPoint { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object
        /// </summary>
        /// <param name="obj">The object to compare with the current object</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false</returns>
        public override bool Equals(object obj)
        {
            return (obj is Receiver receiver && receiver.FriendlyName == FriendlyName &&
                (receiver.IPEndPoint != null && receiver.IPEndPoint.Equals(IPEndPoint) || receiver.IPEndPoint == null && IPEndPoint == null));
        }

        /// <summary>
        /// Serves as the default hash function
        /// </summary>
        /// <returns>A hash code for the current object</returns>
        public override int GetHashCode()
        {
            return IPEndPoint.GetHashCode();
        }
    }
}

using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for the connection channel
    /// </summary>
    public interface IConnectionChannel : IChannel
    {
        /// <summary>
        /// Connects 
        /// </summary>
        /// <param name="destinationId">destination identifier</param>
        Task ConnectAsync(string destinationId = DefaultIdentifiers.DESTINATION_ID);
    }
}
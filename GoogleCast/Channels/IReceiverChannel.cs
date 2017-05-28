using GoogleCast.Models.Receiver;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for the receiver channel
    /// </summary>
    public interface IReceiverChannel : IChannel
    {
        /// <summary>
        /// Launches an application
        /// </summary>
        /// <param name="applicationId">application identifier</param>
        /// <returns>receiver status</returns>
        Task<ReceiverStatus> LaunchAsync(string applicationId);

        /// <summary>
        /// Checks the connection is well established
        /// </summary>
        /// <param name="ns">namespace</param>
        /// <returns>an application object</returns>
        Task<Application> EnsureConnection(string ns);
    }
}
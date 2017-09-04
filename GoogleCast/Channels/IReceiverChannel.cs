using GoogleCast.Models.Receiver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for the receiver channel
    /// </summary>
    public interface IReceiverChannel : IStatusChannel<ReceiverStatus>
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


		/// <summary>
		/// Sets the volume
		/// </summary>
		/// <param name="level">volume level (0.0 to 1.0)</param>
		/// <param name="muted">muted state</param>
		/// <returns>receiver status</returns>
		Task<ReceiverStatus> SetVolumeAsync(float level, bool muted);
    }
}
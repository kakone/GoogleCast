using System;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for a status channel
    /// </summary>
    /// <typeparam name="TStatus">status type</typeparam>
    public interface IStatusChannel<TStatus> : IChannel
    {
        /// <summary>
        /// Raised when the status has changed
        /// </summary>
        event EventHandler StatusChanged;

        /// <summary>
        /// Gets the status
        /// </summary>
        TStatus Status { get; }
    }
}

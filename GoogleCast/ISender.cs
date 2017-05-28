using GoogleCast.Channels;
using GoogleCast.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleCast
{
    /// <summary>
    /// Interface for the GoogleCast sender
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// Raised when the sender is disconnected
        /// </summary>
        event EventHandler Disconnected;

        /// <summary>
        /// Gets a channel
        /// </summary>
        /// <typeparam name="TChannel">channel type</typeparam>
        /// <returns>a channel</returns>
        TChannel GetChannel<TChannel>() where TChannel : IChannel;

        /// <summary>
        /// Connects to a receiver
        /// </summary>
        /// <param name="receiver">receiver</param>
        Task ConnectAsync(IReceiver receiver);

        /// <summary>
        /// Disconnects
        /// </summary>
        Task DisconnectAsync();

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <param name="ns">namespace</param>
        /// <param name="message">message to send</param>
        /// <param name="destinationId">destination identifier</param>
        Task SendAsync(string ns, IMessage message, string destinationId);

        /// <summary>
        /// Sends a message
        /// </summary>
        /// <typeparam name="TResponse">response type</typeparam>
        /// <param name="ns">namespace</param>
        /// <param name="message">message to send</param>
        /// <param name="destinationId">destination identifier</param>
        /// <returns>the result</returns>
        Task<TResponse> SendAsync<TResponse>(string ns, IMessageWithId message, string destinationId)
            where TResponse : IMessageWithId;

        /// <summary>
        /// Gets the differents statuses
        /// </summary>
        /// <returns>a dictionnary of namespace/status</returns>
        IDictionary<string, object> GetStatuses();

        /// <summary>
        /// Restore the differents statuses
        /// </summary>
        /// <param name="statuses">statuses to restore</param>
        void RestoreStatuses(IDictionary<string, object> statuses);
    }
}
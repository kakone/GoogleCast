using GoogleCast.Messages;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for a channel
    /// </summary>
    public interface IChannel
    {
        /// <summary>
        /// Gets or sets the sender
        /// </summary>
        ISender Sender { get; set; }

        /// <summary>
        /// Gets the full namespace
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Called when a message for this channel is received
        /// </summary>
        /// <param name="message">message to process</param>
        Task OnMessageReceivedAsync(IMessage message);
    }
}
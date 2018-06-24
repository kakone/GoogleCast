using GoogleCast.Messages;
using GoogleCast.Messages.Connection;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Connection channel
    /// </summary>
    class ConnectionChannel : Channel, IConnectionChannel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConnectionChannel"/> class
        /// </summary>
        public ConnectionChannel() : base("tp.connection")
        {
        }

        /// <summary>
        /// Connects 
        /// </summary>
        /// <param name="destinationId">destination identifier</param>
        public async Task ConnectAsync(string destinationId)
        {
            await SendAsync(new ConnectMessage(), destinationId);
        }

        /// <summary>
        /// Called when a message for this channel is received
        /// </summary>
        /// <param name="message">message to process</param>
        public async override Task OnMessageReceivedAsync(IMessage message)
        {
            if (message is CloseMessage)
            {
                await Sender.DisconnectAsync();
            }
            await base.OnMessageReceivedAsync(message);
        }
    }
}

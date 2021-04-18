using System.Threading.Tasks;
using GoogleCast.Messages;
using GoogleCast.Messages.Connection;

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

        /// <inheritdoc/>
        public async Task ConnectAsync(string destinationId)
        {
            await SendAsync(new ConnectMessage(), destinationId);
        }

        /// <inheritdoc/>
        public async override Task OnMessageReceivedAsync(IMessage message)
        {
            if (message is CloseMessage)
            {
                Sender!.Disconnect();
            }
            await base.OnMessageReceivedAsync(message);
        }
    }
}

using GoogleCast.Messages;
using GoogleCast.Messages.Heartbeat;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Heartbeat channel
    /// </summary>
    class HeartbeatChannel : Channel, IHeartbeatChannel
    {
        /// <summary>
        /// Initializes a new instance of HeartbeatChannel class
        /// </summary>
        public HeartbeatChannel() : base("tp.heartbeat")
        {
        }

        /// <summary>
        /// Called when a message for this channel is received
        /// </summary>
        /// <param name="message">message to process</param>
        public override async Task OnMessageReceivedAsync(IMessage message)
        {
            switch (message)
            {
                case PingMessage pingMessage:
                    await SendAsync(new PongMessage());
                    break;
            }
        }
    }
}

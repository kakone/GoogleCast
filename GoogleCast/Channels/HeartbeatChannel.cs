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
        /// Initializes a new instance of <see cref="HeartbeatChannel"/> class
        /// </summary>
        public HeartbeatChannel() : base("tp.heartbeat")
        {
        }

        /// <inheritdoc/>
        public override async Task OnMessageReceivedAsync(IMessage message)
        {
            switch (message)
            {
                case PingMessage:
                    await SendAsync(new PongMessage());
                    break;
            }
        }
    }
}

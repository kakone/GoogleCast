using GoogleCast.Messages;
using GoogleCast.Messages.Heartbeat;
using GoogleCast.Models.HeartBeat;
using System;
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

        public event EventHandler<PingEvent>? PingReceived;

        /// <inheritdoc/>
        public override async Task OnMessageReceivedAsync(IMessage message)
        {
            switch (message)
            {
                case PingMessage:
                    PingReceived?.Invoke(this, new PingEvent { Date = DateTime.Now });
                    await SendAsync(new PongMessage());
                    break;
            }
        }
    }
}

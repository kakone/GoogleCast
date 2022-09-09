using System;
using GoogleCast.Models.HeartBeat;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for the heartbeat channel
    /// </summary>
    public interface IHeartbeatChannel : IChannel
    {
        /// <summary>
        /// Event when a ping is received
        /// </summary>
        public event EventHandler<PingEvent> PingReceived;
    }
}

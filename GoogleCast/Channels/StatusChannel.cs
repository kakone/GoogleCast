﻿using GoogleCast.Messages;
using System;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Base class for status channels
    /// </summary>
    /// <typeparam name="TStatusMessage">status message type</typeparam>
    /// <typeparam name="TStatus">status type</typeparam>
    public abstract class StatusChannel<TStatusMessage, TStatus> : Channel, IStatusChannel<TStatus> where TStatusMessage : IStatusMessage<TStatus>
    {
        /// <summary>
        /// Raised when the status has changed
        /// </summary>
        public event EventHandler StatusChanged;

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="ns">namespace</param>
        public StatusChannel(string ns) : base(ns)
        {
        }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public TStatus Status { get; protected set; }

        /// <summary>
        /// Called when a message for this channel is received
        /// </summary>
        /// <param name="message">message to process</param>
        public override Task OnMessageReceivedAsync(IMessage message)
        {
            switch (message)
            {
                case TStatusMessage statusMessage:
                    Status = statusMessage.Status;
                    OnStatusChanged();
                    break;
            }

            return base.OnMessageReceivedAsync(message);
        }

        /// <summary>
        /// Raises the StatusChanged event
        /// </summary>
        protected virtual void OnStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}

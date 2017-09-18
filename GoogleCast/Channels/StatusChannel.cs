using GoogleCast.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Base class for status channels
    /// </summary>
    /// <typeparam name="TStatus">status type</typeparam>
    /// <typeparam name="TStatusMessage">status message type</typeparam>
    /// <typeparam name="TGetStatusMessage">get status message type</typeparam>
    public abstract class StatusChannel<TStatus, TStatusMessage, TGetStatusMessage> : Channel, IStatusChannel<TStatus>
        where TStatusMessage : IStatusMessage<TStatus>
        where TGetStatusMessage : IMessageWithId, new()
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

        private TStatus _status;
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public TStatus Status
        {
            get { return _status; }
            protected set
            {
                if (!EqualityComparer<TStatus>.Default.Equals(_status, value))
                {
                    _status = value;
                    OnStatusChanged();
                }
            }
        }

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

        /// <summary>
        /// Creates a GetStatusMessage
        /// </summary>
        /// <returns>a GetStatusMessage</returns>
        protected virtual TGetStatusMessage CreateGetStatusMessage()
        {
            return new TGetStatusMessage();
        }

        /// <summary>
        /// Check the status
        /// </summary>
        /// <returns>the current status</returns>
        protected async Task<TStatus> CheckStatus()
        {
            var status = Status;
            if (status == null)
            {
                status = await GetStatusAsync();
            }
            return status;
        }

        /// <summary>
        /// Retrieves the status
        /// </summary>
        /// <returns>the status</returns>
        public async Task<TStatus> GetStatusAsync()
        {
            return (await SendAsync<TStatusMessage>(CreateGetStatusMessage())).Status;
        }
    }
}

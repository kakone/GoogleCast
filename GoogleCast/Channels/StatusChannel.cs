using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleCast.Messages;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Base class for status channels
    /// </summary>
    /// <typeparam name="TStatus">status type</typeparam>
    /// <typeparam name="TStatusMessage">status message type</typeparam>
    public abstract class StatusChannel<TStatus, TStatusMessage> : Channel, IStatusChannel<TStatus>
        where TStatusMessage : IStatusMessage<TStatus>
    {
        /// <inheritdoc/>
        public event EventHandler? StatusChanged;

        /// <summary>
        /// Initialization
        /// </summary>a
        /// <param name="ns">namespace</param>
        public StatusChannel(string ns) : base(ns)
        {
        }

        private TStatus? _status;
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        public TStatus? Status
        {
            get => _status;
            private set
            {
                if (!EqualityComparer<TStatus>.Default.Equals(_status!, value!))
                {
                    _status = value;
                    OnStatusChanged();
                }
            }
        }

        object? IStatusChannel.Status
        {
            get => Status;
            set => Status = (TStatus)value!;
        }

        /// <inheritdoc/>
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
    }
}

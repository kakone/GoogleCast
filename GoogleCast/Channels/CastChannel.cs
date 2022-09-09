using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleCast.Messages;
using GoogleCast.Messages.Cast;
using GoogleCast.Messages.Media;
using GoogleCast.Models.Cast;
using GoogleCast.Models.Media;
using GoogleCast.Models.Receiver;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Cast channel
    /// </summary>
    class CastChannel : StatusChannel<IEnumerable<CastStatus>, CastStatusMessage>, ICastChannel
    {
        /// <summary>
        /// Initializes a new instance of <see cref="CastChannel"/> class
        /// </summary>
        public CastChannel() : base("urn:x-cast:com.madmod.dashcast")
        {
        }

        /// <inheritdoc/>
        public string ApplicationId { get; } = "84912283";

        private Task<Application> GetApplicationAsync()
        {
            return Sender!.GetChannel<IReceiverChannel>().EnsureConnectionAsync(Namespace);
        }

        public async Task LoadUrl(CastInformation information)
        {
            var message = new CastLoadMessage
            {
                Url = information.Url,
                Force = false,
                Reload = true,
                ReloadTime = 0,
                SessionId = (await GetApplicationAsync()).SessionId,
            };

            await SendAsync<CastLoadMessage>(message, (await GetApplicationAsync()).TransportId);
        }
    }
}

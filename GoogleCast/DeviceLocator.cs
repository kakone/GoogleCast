using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Zeroconf;

namespace GoogleCast
{
    /// <summary>
    /// Device locator
    /// </summary>
    public class DeviceLocator : IDeviceLocator
    {
        private const string PROTOCOL = "_googlecast._tcp.local.";

        /// <summary>
        /// Find the available receivers
        /// </summary>
        /// <returns>a collection of receivers</returns>
        public async Task<IEnumerable<Receiver>> FindReceiversAsync()
        {
			var receivers = new List<Receiver>();
			await ZeroconfResolver.ResolveAsync(PROTOCOL, callback: c =>
			{
				var service = c.Services[PROTOCOL];
				receivers.Add(new Receiver()
				{
					FriendlyName = service.Properties[0]["fn"],
					IPEndPoint = new IPEndPoint(IPAddress.Parse(c.IPAddress), service.Port)
				});
			});
			return receivers;
		}
    }
}

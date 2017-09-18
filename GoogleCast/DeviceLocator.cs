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
        public async Task<IEnumerable<IReceiver>> FindReceiversAsync()
        {
            var receivers = new List<IReceiver>();
            IService service;
            IReceiver receiver;
            await ZeroconfResolver.ResolveAsync(PROTOCOL, callback: c =>
            {
                service = c.Services[PROTOCOL];
                receiver = new Receiver()
                {
                    FriendlyName = service.Properties[0]["fn"],
                    IPEndPoint = new IPEndPoint(IPAddress.Parse(c.IPAddress), service.Port)
                };
                if (!receivers.Any(r => r.IPEndPoint.Equals(receiver.IPEndPoint)))
                {
                    receivers.Add(receiver);
                }
            });
            return receivers;
        }
    }
}

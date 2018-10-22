using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive.Linq;
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

        private Receiver CreateReceiver(IZeroconfHost host)
        {
            var service = host.Services[PROTOCOL];
            return new Receiver()
            {
                Id = service.Properties[0]["id"],
                FriendlyName = service.Properties[0]["fn"],
                IPEndPoint = new IPEndPoint(IPAddress.Parse(host.IPAddress), service.Port)
            };
        }

        /// <summary>
        /// Finds the available receivers
        /// </summary>
        /// <returns>a collection of receivers</returns>
        public async Task<IEnumerable<IReceiver>> FindReceiversAsync()
        {
            return (await ZeroconfResolver.ResolveAsync(PROTOCOL)).Select(CreateReceiver);
        }

        /// <summary>
        /// Finds the available receivers in continuous way
        /// </summary>
        /// <returns>a provider for notifications</returns>
        public IObservable<IReceiver> FindReceiversContinuous()
        {
            return ZeroconfResolver.ResolveContinuous(PROTOCOL).Select(CreateReceiver);
        }
    }
}

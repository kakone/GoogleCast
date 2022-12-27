using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Zeroconf;

namespace GoogleCast;

/// <summary>
/// Device locator
/// </summary>
public class DeviceLocator : IDeviceLocator
{
    private const string PROTOCOL = "_googlecast._tcp.local.";

    private Receiver CreateReceiver(IZeroconfHost host)
    {
        var service = host.Services[PROTOCOL];
        var properties = service.Properties.First();
        return new Receiver()
        {
            Id = properties["id"],
            FriendlyName = properties["fn"],
            IPEndPoint = new IPEndPoint(IPAddress.Parse(host.IPAddress), service.Port)
        };
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<IReceiver>> FindReceiversAsync(NetworkInterface? networkInterface = null)
    {
        return (await ZeroconfResolver.ResolveAsync(
                PROTOCOL,
                netInterfacesToSendRequestOn: networkInterface == null ? null : new[] { networkInterface }))
            .Select(CreateReceiver);
    }

    /// <inheritdoc/>
    public IObservable<IReceiver> FindReceiversContinuous()
    {
        return ZeroconfResolver.ResolveContinuous(PROTOCOL).Select(CreateReceiver);
    }
}

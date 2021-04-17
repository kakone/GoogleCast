using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace GoogleCast
{
    /// <summary>
    /// Interface for the device locator
    /// </summary>
    public interface IDeviceLocator
    {
        /// <summary>
        /// Finds the available receivers
        /// </summary>
        /// <returns>a collection of receivers</returns>
        Task<IEnumerable<IReceiver>> FindReceiversAsync();

        /// <summary>
        /// Finds the available receivers on network interface
        /// </summary>
        /// <returns>a collection of receivers</returns>
        Task<IEnumerable<IReceiver>> FindReceiversAsync(NetworkInterface networkInterface);
        
        /// <summary>
        /// Finds the available receivers in continuous way
        /// </summary>
        /// <returns>a provider for notifications</returns>
        IObservable<IReceiver> FindReceiversContinuous();
    }
}
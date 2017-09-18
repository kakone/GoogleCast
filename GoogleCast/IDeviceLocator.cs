using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleCast
{
    /// <summary>
    /// Interface for the device locator
    /// </summary>
    public interface IDeviceLocator
    {
        /// <summary>
        /// Find the available receivers
        /// </summary>
        /// <returns>a collection of receivers</returns>
        Task<IEnumerable<IReceiver>> FindReceiversAsync();
    }
}
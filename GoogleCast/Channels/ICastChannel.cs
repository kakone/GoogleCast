using System.Collections.Generic;
using System.Threading.Tasks;
using GoogleCast.Models.Cast;
using GoogleCast.Models.Media;

namespace GoogleCast.Channels
{
    /// <summary>
    /// Interface for the media channel
    /// </summary>
    public interface ICastChannel : IStatusChannel<IEnumerable<CastStatus>>, IApplicationChannel
    {
        /// <summary>
        /// Retrieves the media status
        /// </summary>
        /// <returns>the media status</returns>
        Task LoadUrl(CastInformation information);
    }
}

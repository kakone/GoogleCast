using System;

namespace GoogleCast
{
    /// <summary>
    /// Collection of services
    /// </summary>
    public sealed class ServiceCollection : Microsoft.Extensions.DependencyInjection.ServiceCollection
    {
        private static volatile ServiceCollection _default;
        private static object syncRoot = new Object();

        private ServiceCollection() { }

        /// <summary>
        /// Gets the default instance of this class
        /// </summary>
        public static ServiceCollection Default
        {
            get
            {
                if (_default == null)
                {
                    lock (syncRoot)
                    {
                        if (_default == null)
                        {
                            _default = new ServiceCollection();
                            _default.RegisterServices();
                        }
                    }
                }

                return _default;
            }
        }
    }
}

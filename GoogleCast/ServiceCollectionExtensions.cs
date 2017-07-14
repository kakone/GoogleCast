using GoogleCast.Channels;
using GoogleCast.Messages;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace GoogleCast
{
    /// <summary>
    /// Services registration
    /// </summary>
    static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the services
        /// </summary>
        /// <param name="services">services to register</param>
        /// <returns>the service descriptors collection</returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            RegisterMessages(services);
            services.AddTransient<IChannel, ConnectionChannel>();
            services.AddTransient<IChannel, HeartbeatChannel>();
            services.AddTransient<IChannel, ReceiverChannel>();
            services.AddTransient<IChannel, MediaChannel>();
            return services;
        }

        private static void RegisterMessages(IServiceCollection services)
        {
            var messageInterfaceType = typeof(IMessage);
            foreach (var type in (from t in typeof(ServiceCollectionExtensions).GetTypeInfo().Assembly.GetTypes()
                                  where t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && messageInterfaceType.IsAssignableFrom(t)
                                  select t))
            {
                services.AddTransient(messageInterfaceType, type);
            }
        }
    }
}

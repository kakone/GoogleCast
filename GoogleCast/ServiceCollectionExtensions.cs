using GoogleCast.Channels;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace GoogleCast
{
    /// <summary>
    /// Services registration
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the services
        /// </summary>
        /// <param name="services">services to register</param>
        /// <returns>the service descriptors collection</returns>
        public static IServiceCollection AddGoogleCast(this IServiceCollection services)
        {
            services.AddSingleton<IMessageTypes, MessageTypes>();

            // Add channels
            var channelType = typeof(IChannel);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(t =>
                t.IsClass && !t.IsAbstract && channelType.IsAssignableFrom(t)))
            {
                services.AddTransient(channelType, type);
            }

            return services;
        }
    }
}

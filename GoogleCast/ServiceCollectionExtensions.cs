using GoogleCast.Channels;
using GoogleCast.Messages;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IMessageTypesManager, MessageTypesManager>();
            services.AddScoped<IChannel, ConnectionChannel>();
            services.AddScoped<IChannel, HeartbeatChannel>();
            services.AddScoped<IChannel, ReceiverChannel>();
            services.AddScoped<IChannel, MediaChannel>();
            return services;
        }
    }
}

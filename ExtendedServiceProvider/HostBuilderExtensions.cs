using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExtendedServiceProvider
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseExtendedServiceProvider(this IHostBuilder builder, IServiceProviderResolver? resolver = null)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            var serviceProviderFactory = new ExtendedServiceProviderFactory(resolver);
            return builder
                .UseServiceProviderFactory(serviceProviderFactory)
                .ConfigureServices((ctx, services) =>
                {
                    services.AddSingleton<IStartupFilter>(sp =>
                    {
                        var serviceProvider = serviceProviderFactory.CreateServiceProvider(services);
                        return new ExtendedServiceProvidersFeatureFilter(serviceProvider);
                    });
                });
        }
    }
}

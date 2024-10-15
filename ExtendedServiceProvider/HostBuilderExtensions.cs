using Microsoft.Extensions.Hosting;

namespace ExtendedServiceProvider
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseExtendedServiceProvider(this IHostBuilder builder, IServiceProviderResolver? resolver = null)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            return builder.UseServiceProviderFactory(new ExtendedServiceProviderFactory(resolver));
        }
    }
}

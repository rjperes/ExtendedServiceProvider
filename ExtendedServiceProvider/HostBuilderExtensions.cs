using Microsoft.Extensions.Hosting;

namespace ExtendedServiceProvider
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseExtendedServiceProvider(this IHostBuilder builder)
        {
            return builder.UseServiceProviderFactory(new ExtendedServiceProviderFactory());
        }
    }
}

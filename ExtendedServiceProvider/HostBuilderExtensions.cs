using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExtendedServiceProvider
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseExtendedServiceProvider(this IHostBuilder builder, IServiceProviderResolver? resolver = null)
        {
            ArgumentNullException.ThrowIfNull(builder, nameof(builder));
            builder.ConfigureContainer<IServiceCollection>((ctx, services) =>
            {
                ctx.ToString();
            });
            builder.ConfigureServices((ctx, services) =>
            {
                ctx.ToString();
            });
            return builder.UseServiceProviderFactory(new ExtendedServiceProviderFactory(resolver));
        }
    }
}

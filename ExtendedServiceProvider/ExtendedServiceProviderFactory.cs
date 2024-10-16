using Microsoft.Extensions.DependencyInjection;

namespace ExtendedServiceProvider
{
    internal class ExtendedServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        private IServiceProvider? _serviceProvider;
        private readonly IServiceProviderResolver? _resolver;

        public ExtendedServiceProviderFactory(IServiceProviderResolver? resolver = null)
        {
            _resolver = resolver;
        }

        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            return services;
        }

        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            ArgumentNullException.ThrowIfNull(containerBuilder, nameof(containerBuilder));
            return _serviceProvider is null ? _serviceProvider = new ExtendedServiceProvider(containerBuilder, _resolver) : _serviceProvider;
        }
    }
}

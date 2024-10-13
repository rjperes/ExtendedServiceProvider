using Microsoft.Extensions.DependencyInjection;

namespace ExtendedServiceProvider
{
    internal class ExtendedServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        private readonly IServiceProviderResolver? _resolver;

        class ExtendedServiceProvider : IKeyedServiceProvider
        {
            private readonly IKeyedServiceProvider _serviceProvider;
            private readonly IServiceProviderResolver? _resolver;
            private readonly IEnumerable<IServiceProviderHook> _hooks;

            public ExtendedServiceProvider(IKeyedServiceProvider serviceProvider, IServiceProviderResolver? resolver)
            {
                ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
                _serviceProvider = serviceProvider;
                _resolver = resolver ?? serviceProvider.GetService<IServiceProviderResolver>();
                _hooks = serviceProvider.GetServices<IServiceProviderHook>();
            }

            public object? GetKeyedService(Type serviceType, object? serviceKey)
            {
                ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

                var service = ResolveKeyedService(serviceType, serviceKey) ?? _serviceProvider.GetKeyedService(serviceType, serviceKey);

                if (service != null)
                {                    
                    foreach (var hook in _hooks)
                    {
                        hook.ServiceResolved(serviceType, service);
                    }
                }

                return service;
            }

            private object? ResolveKeyedService(Type serviceType, object? serviceKey)
            {
                return _resolver?.Resolve(_serviceProvider, serviceType, serviceKey);
            }

            public object GetRequiredKeyedService(Type serviceType, object? serviceKey)
            {
                var service = GetKeyedService(serviceType, serviceKey);

                return service == null ? throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.") : service;
            }

            public object? GetService(Type serviceType)
            {
                return GetKeyedService(serviceType, null);
            }
        }

        public ExtendedServiceProviderFactory(IServiceProviderResolver? resolver = null)
        {
            _resolver = resolver;
        }

        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            return services;
        }

        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            return new ExtendedServiceProvider(containerBuilder.BuildServiceProvider(), _resolver);
        }
    }
}

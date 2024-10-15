﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ExtendedServiceProvider
{
    internal class ExtendedServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        private readonly IServiceProviderResolver? _resolver;

        [DebuggerDisplay("ServiceDescriptors = {_services.Count}, Extended")]
        class ExtendedServiceProvider : IKeyedServiceProvider, IServiceProviderIsService, IServiceProviderIsKeyedService, ISupportRequiredService, IServiceScopeFactory
        {
            private readonly IServiceCollection _services;
            private readonly ILogger<ExtendedServiceProvider> _logger;
            private readonly IKeyedServiceProvider _serviceProvider;
            private readonly IServiceProviderResolver? _resolver;
            private readonly IEnumerable<IServiceProviderHook> _hooks;
            private static readonly Type[] _targetTypes = [typeof(IServiceProvider), typeof(IKeyedServiceProvider), typeof(IServiceProviderIsService), typeof(IServiceProviderIsKeyedService), typeof(ISupportRequiredService), typeof(IServiceScopeFactory)];

            public ExtendedServiceProvider(IServiceCollection services, IServiceProviderResolver? resolver, ServiceProviderOptions options)
            {
                ArgumentNullException.ThrowIfNull(services, nameof(services));
                _services = services;
                _serviceProvider = services.BuildServiceProvider(options);
                _logger = _serviceProvider.GetRequiredService<ILogger<ExtendedServiceProvider>>();
                _resolver = resolver ?? _serviceProvider.GetService<IServiceProviderResolver>();
                _hooks = _serviceProvider.GetServices<IServiceProviderHook>();
            }

            public IServiceCollection Services => _services;

            public object? GetKeyedService(Type serviceType, object? serviceKey)
            {
                ArgumentNullException.ThrowIfNull(serviceType, nameof(serviceType));

                _logger.LogDebug($"GetKeyedService: serviceType: {serviceType}, serviceKey: {serviceKey}");

                if (_targetTypes.Contains(serviceType) && serviceKey is null)
                {
                    return this;
                }

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

                return service is null ? throw new InvalidOperationException($"No service for type '{serviceType}' has been registered.") : service;
            }

            public object? GetService(Type serviceType)
            {
                return GetKeyedService(serviceType, null);
            }

            public object GetRequiredService(Type serviceType)
            {
                return GetRequiredKeyedService(serviceType, null);
            }

            public bool IsService(Type serviceType)
            {
                return IsKeyedService(serviceType, null);
            }

            public bool IsKeyedService(Type serviceType, object? serviceKey)
            {
                return _services.Any(x => x.ServiceType == serviceType && x.ServiceKey == serviceKey);
            }

            public IServiceScope CreateScope()
            {
                return _serviceProvider.CreateScope();
            }
        }

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
            return new ExtendedServiceProvider(containerBuilder, _resolver, new ServiceProviderOptions { ValidateScopes = false, ValidateOnBuild = true });
        }
    }
}

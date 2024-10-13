using ExtendedServiceProvider;

namespace WebApplication1
{
    internal class CustomServiceProviderResolver : IServiceProviderResolver
    {
        public object? Resolve(IKeyedServiceProvider serviceProvider, Type serviceType, object? serviceKey)
        {
            return serviceProvider.GetKeyedService(serviceType, serviceKey);
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ExtendedServiceProvider
{
    public interface IServiceProviderHook
    {
        void ServiceResolved(Type serviceType, object service);
    }

    public sealed class PropertyInitializerServiceProviderHook : IServiceProviderHook
    {
        private readonly IServiceProvider _serviceProvider;

        public PropertyInitializerServiceProviderHook(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void ServiceResolved(Type serviceType, object service)
        {
            foreach (var prop in service.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.GetProperty))
            {
                var injectPropertyAttribute = prop.GetCustomAttribute<InjectAttribute>();

                if (injectPropertyAttribute != null)
                {
                    if (prop.GetValue(service) == null)
                    {
                        var propertyService = _serviceProvider.GetRequiredKeyedService(prop.PropertyType, injectPropertyAttribute.ServiceKey);
                        prop.SetValue(service, propertyService);
                    }
                }
            }
        }
    }
}
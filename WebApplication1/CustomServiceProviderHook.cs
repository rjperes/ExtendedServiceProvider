using ExtendedServiceProvider;

namespace WebApplication1
{
    internal class CustomServiceProviderHook : IServiceProviderHook
    {
        public void ServiceResolved(Type serviceType, object service)
        {
            if (serviceType == typeof(IMyType))
            {
                serviceType.ToString();
            }
        }
    }
}
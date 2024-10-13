namespace ExtendedServiceProvider
{
    public interface IServiceProviderHook
    {
        void ServiceResolved(Type serviceType, object service);
    }
}

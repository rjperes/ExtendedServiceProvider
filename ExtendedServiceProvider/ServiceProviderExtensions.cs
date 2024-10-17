using Microsoft.Extensions.DependencyInjection;

namespace ExtendedServiceProvider
{
    public static class ServiceProviderExtensions
    {
        public static GenericLazy<T> GetLazyService<T>(this IServiceProvider serviceProvider) where T : class
        {
            return new GenericLazy<T>(() => serviceProvider.GetService<T>()!);
        }

        public static GenericLazy<T> GetLazyKeyedService<T>(this IServiceProvider serviceProvider, object serviceKey) where T : class
        {
            return new GenericLazy<T>(() => serviceProvider.GetKeyedService<T>(serviceKey)!);
        }

        public static GenericLazy<T> GetLazyRequiredService<T>(this IServiceProvider serviceProvider) where T : class
        {
            return new GenericLazy<T>(() => serviceProvider.GetRequiredService<T>());
        }

        public static GenericLazy<T> GetLazyRequiredKeyedService<T>(this IServiceProvider serviceProvider, object serviceKey) where T : class
        {
            return new GenericLazy<T>(() => serviceProvider.GetRequiredKeyedService<T>(serviceKey));
        }
    }
}

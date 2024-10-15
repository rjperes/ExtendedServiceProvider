using ExtendedServiceProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ConsoleApp1
{
    class CustomHook : IServiceProviderHook
    {
        public void ServiceResolved(Type serviceType, object service)
        {
            service.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            services.AddOptions();

            services.AddLogging(options =>
            {
                options.AddConfiguration(configuration.GetSection("Logging"));
                options.AddConsole();
            });

            services.AddServiceProviderHook<CustomHook>();
            services.AddServiceProviderHook<PropertyInitializerServiceProviderHook>();
            services.AddSingleton<IMyType, MyType>();
            services.AddSingleton<string>("ABCD");

            var serviceProvider = services.BuildExtendedServiceProvider();

            var type1 = serviceProvider.GetService<IMyType>();
            var type2 = serviceProvider.GetService<IMyType>();
        }
    }
}

using Microsoft.AspNetCore.Builder;

namespace ExtendedServiceProvider
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExtendedProvider(this IApplicationBuilder app, ServiceProviderOptions? options = null)
        {
            return app.UseMiddleware<ExtendedServiceProviderMiddleware>(options);
        }
    }
}

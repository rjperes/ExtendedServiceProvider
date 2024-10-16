using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace ExtendedServiceProvider
{
    internal class ExtendedServiceProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IExtendedServiceProvider _serviceProvider;

        public ExtendedServiceProviderMiddleware(RequestDelegate next, IExtendedServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //var feature = new RequestServicesFeature(context, _serviceProvider) { RequestServices = _serviceProvider };
            //context.Features.Set(feature);
            await _next(context);
        }
    }
}
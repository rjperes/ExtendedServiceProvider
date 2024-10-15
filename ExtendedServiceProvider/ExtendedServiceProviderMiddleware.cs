using Microsoft.AspNetCore.Http;

namespace ExtendedServiceProvider
{
    internal class ExtendedServiceProviderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServiceProvider _serviceProvider;

        public ExtendedServiceProviderMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.RequestServices = _serviceProvider;
            await _next(context);
        }
    }
}
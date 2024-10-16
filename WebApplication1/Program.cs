using ExtendedServiceProvider;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseExtendedServiceProvider();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddServiceProviderHook<PropertyInitializerServiceProviderHook>();
            builder.Services.AddServiceProviderResolver<CustomServiceProviderResolver>();
            builder.Services.AddSingleton<IMyType, MyType>();
            builder.Services.AddSingleton<string>("ABCD");

            var app = builder.Build();

            //app.Services.GetRequiredKeyedService<string>("ABCD");
            //app.Services.GetService<IMyType>();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

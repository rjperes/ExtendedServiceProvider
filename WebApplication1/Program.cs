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
            builder.Services.AddSingleton<IServiceProviderHook, CustomServiceProviderHook>();
            builder.Services.AddSingleton<IServiceProviderResolver, CustomServiceProviderResolver>();
            builder.Services.AddSingleton<IMyType, MyType>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.Services.GetRequiredKeyedService<string>("aaa");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapDefaultControllerRoute();

            app.Run();
        }
    }
}

using NetMedWebApi.Infrastructure.Loggin.Base;
using NetMedWebApi.Infrastructure.Loggin.Interfaces;
using NetMedWebApi.Infrastructure.MessageJson;
using NetMedWebApi.Infrastructure.MessageJson.interfaces;
using NetMedWebApi.IOC.Dependencies;


namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();
            builder.Services.AddNotificationDependency(builder.Configuration);
            builder.Services.AddRolesDependency(builder.Configuration);
            builder.Services.AddStatusDependency(builder.Configuration);

            builder.Services.AddSingleton<ILoggerCustom, LoggerCustom>();

            builder.Services.AddSingleton<IJsonMessage, JsonMessage>();

            builder.Services.AddScoped(typeof(ILoggerCustom), typeof(LoggerCustom));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
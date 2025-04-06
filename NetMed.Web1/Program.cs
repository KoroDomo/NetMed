using NetMed.ApiConsummer.Infraestructure.Validator.Implementacions;
using NetMed.ApiConsummer.Infraestructure.Validator.Interfaces;
using NetMed.ApiConsummer.IOC.Dependencies;
using NetMed.ApiConsummer.Persistence.Logger;

namespace NetMed.ApiConsummer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();
            builder.Services.AddInsuranceProviderDependencies(builder.Configuration);
            builder.Services.AddNetworkTypeDependencies(builder.Configuration);
            builder.Services.AddSingleton<ICustomLogger, CustomLogger>();

            builder.Services.AddSingleton<IMessageService, MessageService>();

            builder.Services.AddScoped(typeof(ICustomLogger), typeof(CustomLogger));

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

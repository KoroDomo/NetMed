using Microsoft.EntityFrameworkCore;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators;
using NetMed.IOC.Dependencies;
using NetMed.Persistence.Context;

namespace NetMed.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<NetMedContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));

            builder.Services.AddScoped<ICustomLogger, CustomLogger>();

            builder.Services.AddInsuranceProviderDependency();
            builder.Services.AddNetworkTypeDependency();

            builder.Services.AddScoped<JsonMessage>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddLogging();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
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

using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using NetMed.IOC.Dependencies;
using NetMed.Persistence.Interfaces;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators;

namespace NetMed.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<NetMedContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));

            builder.Services.AddScoped<ICustomLogger, CustomLogger>();

            builder.Services.AddInsuranceProviderDependency();
            builder.Services.AddNetworkTypeDependency();

            builder.Services.AddScoped<JsonMessage>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddLogging();



            builder.Services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

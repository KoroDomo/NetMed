using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.Persistence.Repository;

namespace NetMed.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<NetMedContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));

            builder.Services.AddScoped<IInsuranceProviderRepository, InsuranceProviderRepository>();
            builder.Services.AddScoped<INetworkTypeRepository, NetworkTypeRepository>();

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

using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.IOC;
using NetMed.IOC.Dependecies;
using NetMed.IOC.Dependecies.Medical.AvailabilityModes;
using NetMed.IOC.Dependecies.Medical.MedicalRecords;
using NetMed.IOC.Dependecies.Medical.Specialties;

namespace NetMed.Api
{
    public class Program
    {
        public static void Main(string[] args)

            //Proximo: Integracion de Capa de Business Layer con Capa de API
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<NetMedContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NetMedDB")));

            builder.Services.AddSpecialtiesDependency();
            builder.Services.AddMedicalRecordsDependency();
            builder.Services.AddAvailabilityModesDependency();
            //builder.Services.AddRepositories();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
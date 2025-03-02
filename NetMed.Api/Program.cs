using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;
using NetMed.IOC.Dependencies;
using NetMed.IOC;


namespace NetMed.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<NetMedContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));

            // Register application services
            builder.Services.RegisterServices();

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
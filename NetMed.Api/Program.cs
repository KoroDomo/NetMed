using Microsoft.EntityFrameworkCore;
using NetMed.Persistence.Context;
using NetMed.IOC;
using NetMed.Persistence.BaseLoger.Interface;
using NetMed.Persistence.BaseLoger.Loger;
using NetMed.Infrastructure.Mapper.IRepositoryErrorMapper;
using NetMed.Infrastructure.Mapper.RepositoryErrorMapper;


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

            builder.Services.AddSingleton<ILoggerCustom, Loger>();

            builder.Services.AddSingleton<IRepErrorMapper, RepErrorMapper>();

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
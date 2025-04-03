using Microsoft.EntityFrameworkCore;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Infraestructure.Loggin.Base;
using NetMed.IOC.Dependencies;
using NetMed.Persistence.Context;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<NetmedContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));


builder.Services.AddNotificationDependency();
builder.Services.AddRolesDependency();
builder.Services.AddStatusDependency();


builder.Services.AddSingleton<ILoggerCustom, LoggerCustom>();

builder.Services.AddSingleton<JsonMessage>();

builder.Services.AddControllers();
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

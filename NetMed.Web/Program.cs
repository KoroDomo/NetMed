using Microsoft.EntityFrameworkCore;
using NetMed.Application.Interfaces;
using NetMed.Domain.Base;
using NetMed.Infraestructure.Loggin.Base;
using NetMed.Persistence.Context;
using NetMed.IOC.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<NetmedContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));


builder.Services.AddNotificationDependency();
builder.Services.AddRolesDependency();
builder.Services.AddStatusDependency();


builder.Services.AddSingleton<ILoggerCustom, LoggerCustom>();

builder.Services.AddSingleton<JsonMessage>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

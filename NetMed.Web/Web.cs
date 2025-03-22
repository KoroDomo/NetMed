using Microsoft.EntityFrameworkCore;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Validators;
using NetMed.IOC.Dependencies;
using NetMed.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<NetMedContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MedicalAppointment")));

builder.Services.AddScoped<ICustomLogger, CustomLogger>();

builder.Services.AddInsuranceProviderDependency();
builder.Services.AddNetworkTypeDependency();

builder.Services.AddScoped<JsonMessage>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

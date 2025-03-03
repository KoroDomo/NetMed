
using Microsoft.Extensions.DependencyInjection;
using NetMed.Application.Contracts;
using NetMed.Application.Services;
using NetMed.Persistence.Context.Interfaces;
using NetMed.Persistence.Repositories;


namespace NetMed.IOC.Dependencies
{
   public static class NotificationDependency
    {
        public static void AddNotificationDependency(this IServiceCollection service)
        {

            service.AddScoped<INotificationRepository, NotificationRepository>();
            service.AddTransient<INotificationContract, NotificationServices>();




        }
       
        
        
        
    }
}

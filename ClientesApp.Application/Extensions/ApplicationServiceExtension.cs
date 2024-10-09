using ClientesApp.Application.Interfaces.Applications;
using ClientesApp.Application.Mappings;
using ClientesApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ClienteProfileMap));

            services.AddTransient<IClienteAppService, ClienteAppService>();
            services.AddMediatR(m=>m.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            return services;
        }
    }
}

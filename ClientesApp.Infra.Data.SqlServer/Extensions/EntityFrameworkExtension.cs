using ClienteApp.Domain.Interfaces.Repositories;
using ClientesApp.Infra.Data.SqlServer.Context;
using ClientesApp.Infra.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options=> 
            options.UseSqlServer(configuration.GetConnectionString("ClientesApp")));

            services.AddTransient<IClienteRepository, ClienteRepository>();
            return services;
        }
    }
}

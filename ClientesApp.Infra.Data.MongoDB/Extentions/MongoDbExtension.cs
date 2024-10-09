using AutoMapper;
using ClientesApp.Application.Interfaces.Logs;
using ClientesApp.Infra.Data.MongoDB.Contexts;
using ClientesApp.Infra.Data.MongoDB.Settings;
using ClientesApp.Infra.Data.MongoDB.Storages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.MongoDB.Extentions
{
    public static class MongoDbExtension
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoDBSettings = new MongoDBSettings();
            new ConfigureFromConfigurationOptions<MongoDBSettings>
                (configuration.GetSection("MongoDBSettings"))
                .Configure(mongoDBSettings);

            services.AddSingleton(mongoDBSettings);
            services.AddScoped<MongoDBContext>();
            services.AddTransient<ILogClienteDataStore, LogClienteDataStore>();

            return services;
        }
    }
}

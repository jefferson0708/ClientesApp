using ClienteApp.Domain.Entities;
using ClienteApp.Domain.Interfaces.Services;
using ClienteApp.Domain.Services;
using ClienteApp.Domain.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteApp.Domain.Extensions
{
    public static class DomainServiceExtension
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteDomainService, ClienteDomainService>();
            services.AddTransient<IValidator<Cliente>, ClienteValidator>();

            return services;
        }
    }
}

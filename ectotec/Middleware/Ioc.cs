using ectotec.Negocio.Implementations;
using ectotec.Negocio.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ectotec.Middleware
{
    public static class Ioc
    {
        public static IServiceCollection AddDepencency(this IServiceCollection services)
        {
            services.AddScoped<IContactoService, ContactoService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IGeoNameService, GeoNameService>();
            return services;
        }
    }
}

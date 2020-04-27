using Howru.Core.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Howru.Configurations
{
    public static class ConfigureConnection
    {
        public static IServiceCollection AddConnectionProvider
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MyContext>(opt =>
            opt.UseMySql(
                configuration.GetConnectionString("Identity"),
                b => b.MigrationsAssembly("Howru")));

            return services;
        }
    }
}

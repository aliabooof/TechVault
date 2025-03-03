using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Interfaces;
using TechVault.Infrastructure.Repositories;

namespace TechVault.Infrastructure
{
    public static class InfrasstructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<ICategoryRepository,CategoryRepository>();

            return services;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Interfaces;
using TechVault.Infrastructure.Data;
using TechVault.Infrastructure.Repositories;

namespace TechVault.Infrastructure
{
    public static class InfrasstructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            //services.AddScoped<ICategoryRepository,CategoryRepository>();
            //services.AddScoped<IProductRepository,ProductRepository>();
            //services.AddScoped<IPhotoRepository,PhotoRepository>();
            services.AddScoped<IUnitOfWork, IUnitOfWork>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LocalStrings"));
            });
            return services;
        }
    }
}

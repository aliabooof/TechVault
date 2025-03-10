using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Interfaces;
using TechVault.Core.Services;
using TechVault.Infrastructure.Data;
using TechVault.Infrastructure.Repositories;
using TechVault.Infrastructure.Repositories.Services;

namespace TechVault.Infrastructure
{
    public static class InfrasstructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration)
        {
            

            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("LocalStrings"));
            });
            return services;
        }
    }
}
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Data.Repositories;
using Persistence.Identity;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration )
        {


            services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            services.AddScoped<IDbInitializer, DbInitializer>(); // Allow DI Form DbInitializer
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Allow DI Form UnitOfWork
            services.AddScoped<IBasketRepository, BasketRepository>(); // Allow DI Form UnitOfWork
            services.AddScoped<ICacheRepository, CacheRepository>(); // Allow DI Form UnitOfWork
            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

            return services;
        }


    }
}

using ECommerceBackEnd.Contracts;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Entities;
using ECommerceBackEnd.Repositories;
using ECommerceBackEnd.Service.Contracts;
using ECommerceBackEnd.Service;
using System.Runtime.CompilerServices;

namespace ECommerceBackEnd
{
    public static class Extension
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builders =>
                builders.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            });
        public static void ConfigureIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options =>
        {

        });
        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
    }
}

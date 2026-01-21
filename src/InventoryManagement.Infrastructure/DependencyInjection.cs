using InventoryManagement.Application.Interfaces;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagement.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register infrastructure services, e.g., database context, repositories, etc.
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}

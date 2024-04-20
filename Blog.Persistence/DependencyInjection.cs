using Blog.Application.Common.Interfaces;
using Blog.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //// Interceptors
        //services.AddScoped<AuditableEntitySaveChangesInterceptor>();


        // DB
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // DI
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton(TimeProvider.System);

        return services;
    }

}

using Blog.Application.Common.Interfaces;
using Blog.Persistence.Context;
using Blog.Persistence.Inceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Persistence;

public static class ServiceRegistration
{
    public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        //inceptors
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        //postgre db
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("dbcon")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        //services DI
        //services.AddScoped<IUserService, UserService>();
        //services.AddScoped<IAuthService, AuthService>();
        //services.AddScoped<IUserRefreshTokenService, UserRefreshTokenService>();
        //services.AddScoped<IFamilyService, FamilyService>();
        //services.AddScoped<ISubFamilyService, SubFamilyService>();
        //services.AddScoped<IUserFamilyService, UserFamilyService>();
    }
}
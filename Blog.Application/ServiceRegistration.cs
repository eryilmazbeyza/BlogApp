using Blog.Application.Behaviours;
using Blog.Application.Common.Security.Jwt;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //automapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        //mediatr
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<ITokenService, TokenService>();
        //pipeline
        services.AddScoped<IPipelineManagerService, PipelineManagerService>();


        return services;
    }
}

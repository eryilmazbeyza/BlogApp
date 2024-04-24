using Blog.Api.Services;
using Blog.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.ComponentModel.Design;

namespace Blog.Api;

public static class ServiceRegistration
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        // httpden kaynaklı problemleri daha açık bir şekilde açıklar
        services.AddProblemDetails();

        // DI
        services.AddSingleton<ICurrentUserService, CurrentUserService>();
        //services.AddScoped<IHttpService, HttpService>();

        // token içerisindeki bilgileri ulaşmak için
        services.AddHttpContextAccessor();

        // swagger
        services.AddApiVersioning((options) =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        services.AddEndpointsApiExplorer();
        var securityScheme = new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "PUT ONLY TOKEN WITHOUT BEARER HERE!",
        };
        var securityReq = new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                Array.Empty<string>()
            }
        };


        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", securityScheme);
            c.AddSecurityRequirement(securityReq);

            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blog - V1", Version = "v1" });
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "Blog - V2", Version = "v2" });

            // swagger üzerinde summaryler görünmesi için
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
            }
        });


        return services;
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.Application.Common.Security.Jwt;

public static class CustomTokenAuth
{
    public static void AddCustomTokenAuth(this IServiceCollection services, JwtOptions configuration, string schema = JwtBearerDefaults.AuthenticationScheme)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = schema;
            options.DefaultChallengeScheme = schema;
        })
        .AddJwtBearer(schema, options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,            //Oluşturulacak token değerini kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir
                ValidateIssuer = true,              //Oluşturulacak token değerini kimin dağıttını ifade edeceğimiz alandır
                ValidateLifetime = true,            //Oluşturulan token değerinin süresini kontrol edecek olan doğrulamadır.
                ValidateIssuerSigningKey = true,    //Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden security key verisinin doğrulanmasıdır.

                // ClockSkew = TimeSpan.Zero,       //tokena ömür verildiğinde extra default 5 dk verir.Vermesin
                ValidAudience = configuration.Audience[0],
                ValidIssuer = configuration.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.SecretKey ?? string.Empty)),
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow,
            };
        });
    }
}
using Blog.Api;
using Blog.Application;
using Blog.Application.Behaviours;
using Blog.Application.Common.Security;
using Blog.Application.Common.Security.Jwt;
using Blog.Persistence;
using MediatR;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// option pattern
builder.Services.Configure<JwtOptions>(
    builder.Configuration.GetSection("JwtOptions"));

// jwt extensions
var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>()!;
builder.Services.AddCustomTokenAuth(jwtOptions);


//services
builder.Services.AddWebUIServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);


// mediatr
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

});

builder.Services.AddCors(o => o.AddPolicy("default", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

// swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.DisplayRequestDuration();
        c.SwaggerEndpoint($"/swagger/v1/swagger.json", $"v1");
        c.SwaggerEndpoint($"/swagger/v2/swagger.json", $"v2");
        c.DocExpansion(DocExpansion.None);
    });
}
app.UseCors("default");

app.UseHttpsRedirection();

// yetkilendirme jwt
app.UseAuthentication();
// doðrulama
app.UseAuthorization();
app.MapControllers();

app.Run();

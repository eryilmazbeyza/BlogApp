using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Security.Jwt;

namespace Blog.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long UserId => long.TryParse(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == JwtClaimNames.UserId)?.Value, out var userId) ? userId : 0;
    public string? Email => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == JwtClaimNames.Email)?.Value;
    public string? Firstname => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == JwtClaimNames.Firstname)?.Value;
    public string? Lastname => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == JwtClaimNames.Lastname)?.Value;
}
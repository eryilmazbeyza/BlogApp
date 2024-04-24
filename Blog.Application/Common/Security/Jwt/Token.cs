
namespace Blog.Application.Common.Security.Jwt;
public class Token
{
    public string AccessToken { get; set; } = null!;
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiration { get; set; }
}


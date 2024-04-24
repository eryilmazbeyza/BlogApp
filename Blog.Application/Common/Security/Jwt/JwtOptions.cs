

namespace Blog.Application.Common.Security;

public class JwtOptions
{
    public List<string> Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int AccessTokenExpiration { get; set; }
    public int? RefreshTokenExpiration { get; set; }
}

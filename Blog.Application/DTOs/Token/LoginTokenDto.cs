using Blog.Application.Common.Mappings;

namespace Blog.Application.DTOs.Token;

public class LoginTokenDto : IMapFrom<Common.Security.Jwt.Token>
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
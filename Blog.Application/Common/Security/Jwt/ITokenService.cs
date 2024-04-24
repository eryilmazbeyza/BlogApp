
using System.Security.Claims;

namespace Blog.Application.Common.Security.Jwt;

public interface ITokenService
{
    IEnumerable<Claim> GetClaims(TokenUserDto tokenDto, List<string> audiences);
    Token CreateAccessToken(TokenUserDto tokenDto);
    string CreateRefreshToken();
}
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Application.Common.Security.Jwt;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    public TokenService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;

    }

    public IEnumerable<Claim> GetClaims(TokenUserDto tokenDto, List<string> audiences)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, tokenDto.Id.ToString()),
            new Claim(JwtClaimNames.UserId, tokenDto.Id.ToString()),
            new Claim(JwtClaimNames.Firstname, tokenDto.Firstname),
            new Claim(JwtClaimNames.Lastname, tokenDto.Lastname),
            new Claim(JwtClaimNames.Email, tokenDto.Email)
        };

        //audienceler ekleniyor.
        //Aud = Api'a istek yapıldığında, token'ı istek yapılmaya uygun mu diye kontrol ediyor
        var claimList = audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience));
        claims.AddRange(claimList);
        return claims;
    }

    public Token CreateAccessToken(TokenUserDto tokenDto)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(_jwtOptions.AccessTokenExpiration); //şu anki zaman dilime ilave zaman dilimi ekler
        var refreshTokenExpiration = DateTime.Now.AddMinutes(_jwtOptions.RefreshTokenExpiration.Value); //şu anki zaman dilime ilave zaman dilimi ekler

        SymmetricSecurityKey securityKey = GetSymmetricSecurityKey(_jwtOptions.SecretKey); // tokenı imzalayacak olan key
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature); // imzamız

        //Oluşturulacak token ayarlarını veriyoruz.
        JwtSecurityToken securityToken = new(
            issuer: _jwtOptions.Issuer, //toke'nı yayınlayan kim,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now, //vermiş olduğum dakikadan önce geçersiz olmasın    token süresi = notBefore ile expires arası
            signingCredentials: signingCredentials, //imzamız
            claims: GetClaims(tokenDto, _jwtOptions.Audience) //token hangi apilere ulaşabilsin.
        );

        //Token oluşturucu sınıfından bir örnek alalım.
        JwtSecurityTokenHandler tokenHandler = new();
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new Token
        {
            AccessToken = accessToken,
            AccessTokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };
    }

    public string CreateRefreshToken()
    {
        var number = new byte[32];
        using var random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }

    private static SymmetricSecurityKey GetSymmetricSecurityKey(string securityKey) =>
        new(Encoding.UTF8.GetBytes(securityKey));
}

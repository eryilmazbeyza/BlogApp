
namespace Blog.Application.Common.Security.Jwt;

public class TokenUserDto
{
    public long Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
}

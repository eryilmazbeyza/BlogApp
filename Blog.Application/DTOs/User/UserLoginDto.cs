using Blog.Application.Common.Mappings;
using Blog.Application.DTOs.Token;

namespace Blog.Application.DTOs.User;

public class UserLoginDto : IMapFrom<Domain.Entities.User>
{
    public long Id { get; set; }
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public LoginTokenDto? Token { get; set; }
}
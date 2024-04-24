
namespace Blog.Application.Common.Interfaces;

public interface ICurrentUserService
{
    long UserId { get; }
    string? Email { get; }
    string? Firstname { get; }
    string? Lastname { get; }
}
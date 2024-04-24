using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using Blog.Application.Common.Security;
using MediatR;

namespace Blog.Application.Features.User.Commands;

[AllowAnonymous]
public record CreateUserCommand : IRequest<Result<long>>
{
    public string Firstname { get; init; } = null!;
    public string Lastname { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}

internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.Entities.User
        {
            Firstname = request.Firstname,
            Lastname = request.Lastname,
            Email = request.Email,
            Password = Crypto.PasswordHash(request.Password)
        };

        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<long>.Success(user.Id,ResultMessages.Success);

    }
}
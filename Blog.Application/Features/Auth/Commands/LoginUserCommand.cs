using AutoMapper;
using Blog.Application.Common.Context;
using Blog.Application.Common.Models;
using Blog.Application.Common.Security;
using Blog.Application.Common.Security.Jwt;
using Blog.Application.DTOs.Token;
using Blog.Application.DTOs.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Auth.Commands;


[Common.Security.AllowAnonymous]
public record UserLoginCommand : IRequest<Result<UserLoginDto>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
}

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<UserLoginDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;


    public UserLoginCommandHandler(IApplicationDbContext context, ITokenService tokenService, IMapper mapper)
    {
        _context = context;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<Result<UserLoginDto>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(x => 
                x.Email.ToLower() == request.Email.ToLower()
                && x.Password == Crypto.PasswordHash(request.Password), cancellationToken);

        if (user is null)
            throw new Exception("E-Posta adresiniz veya şifreniz hatalı.");

        var token = _tokenService.CreateAccessToken(new TokenUserDto
        {
            Id = user.Id,
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Email = user.Email
        });

        var result = _mapper.Map<UserLoginDto>(user);
        result.Token = _mapper.Map<LoginTokenDto>(token);

        return Result<UserLoginDto>.Success(result);
    }

}

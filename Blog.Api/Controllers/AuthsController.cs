using Blog.Application.Common.Models;
using Blog.Application.DTOs.User;
using Blog.Application.Features.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiVersion("1.0")]
public class AuthController : ApiControllerBase
{
    /// <summary>
    /// sadece login olduğunda kullanılır.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<Result<UserLoginDto>>> Login(UserLoginCommand command) =>
        await Mediator.Send(command);


}
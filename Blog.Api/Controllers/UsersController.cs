using Blog.Application.Common.Models;
using Blog.Application.Features.User.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiVersion("1.0")]

public class UsersController : ApiControllerBase
{

    /// <summary>
    /// kullanıcı kayıt
    /// </summary>
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<Result<long>>> Create(CreateUserCommand command) =>
        await Mediator.Send(command);

}

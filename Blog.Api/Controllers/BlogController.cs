using Blog.Application.Common.Models;
using Blog.Application.Features.Blog.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiVersion("1.0")]

public class BlogController : ApiControllerBase
{

    [HttpPost]
    public async Task<ActionResult<Result<long>>> Create(CreateBlogCommand command) =>
        await Mediator.Send(command);
}

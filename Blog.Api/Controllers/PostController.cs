using Blog.Application.Common.Models;
using Blog.Application.Features.Post.Commands.Create;
using Blog.Application.Features.Post.Commands.Delete;
using Blog.Application.Features.Post.Commands.Update;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiVersion("1.0")]

public class PostController : ApiControllerBase
{

    [HttpGet("PostListWithPagination")]
    public async Task<ActionResult<Result<PaginatedList<PostListDto>>>> PostListWithPagination() =>
        await Mediator.Send(new GetPostListWithPaginationQuery());

    [HttpPost]
    public async Task<ActionResult<Result<long>>> Create(CreatePostCommand command) =>
        await Mediator.Send(command);

    [HttpPut]
    public async Task<ActionResult<Result>> Update(UpdatePostCommand command) =>
        await Mediator.Send(command);

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Result>> Delete([FromRoute] DeletePostCommand command) =>
        await Mediator.Send(command);
}

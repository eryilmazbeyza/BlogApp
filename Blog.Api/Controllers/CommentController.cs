using Blog.Application.Common.Models;
using Blog.Application.Features.Comment.Commands.Create;
using Blog.Application.Features.Comment.Commands.Delete;
using Blog.Application.Features.Comment.Commands.Update;
using Blog.Application.Features.Comment.Queries.CommentListWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiVersion("1.0")]
public class CommentController : ApiControllerBase
{
    [HttpGet("CommentListWithPagination")]
    public async Task<ActionResult<Result<PaginatedList<CommentListDto>>>> CommentListWithPagination() =>
    await Mediator.Send(new CommentListWithPaginationQuery());

    [HttpPost]
    public async Task<ActionResult<Result<long>>> Create(CreateCommentCommand command) =>
        await Mediator.Send(command);

    [HttpPut]
    public async Task<ActionResult<Result>> Update(UpdateCommentCommand command) =>
        await Mediator.Send(command);

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Result>> Delete([FromRoute] DeleteCommentCommand command) =>
        await Mediator.Send(command);
}

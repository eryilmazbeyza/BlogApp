using Blog.Application.Common.Models;
using Blog.Application.Features.Category.Commands.Create;
using Blog.Application.Features.Category.Commands.Delete;
using Blog.Application.Features.Category.Commands.Update;
using Blog.Application.Features.Category.Queries;
using Blog.Application.Features.Category.Queries.CategoryListWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[ApiVersion("1.0")]
public class CategoryController : ApiControllerBase
{
    [HttpGet("CategoryListWithPagination")]
    public async Task<ActionResult<Result<PaginatedList<CategoryListDto>>>> CategoryListWithPagination() =>
    await Mediator.Send(new CategoryListWithPaginationQuery());

    [HttpPost]
    public async Task<ActionResult<Result<long>>> Create(CreateCategoryCommand command) =>
        await Mediator.Send(command);

    [HttpPut]
    public async Task<ActionResult<Result>> Update(UpdateCategoryCommand command) =>
        await Mediator.Send(command);

    [HttpDelete("{Id}")]
    public async Task<ActionResult<Result>> Delete([FromRoute] DeleteCategoryCommand command) =>
        await Mediator.Send(command);
}

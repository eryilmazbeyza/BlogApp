using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Blog.Application.Features.Category.Queries.CategoryListWithPagination;

public record CategoryListWithPaginationQuery : IRequest<Result<PaginatedList<CategoryListDto>>>
{
    public int PageSize { get; init; } = 50;
    public int PageNumber { get; init; } = 1;
    public string Sort { get; init; } = "Id desc";

}

internal sealed class CategoryListWithPaginationQueryHandler : IRequestHandler<CategoryListWithPaginationQuery, Result<PaginatedList<CategoryListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CategoryListWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CategoryListDto>>> Handle(CategoryListWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var comm = _context.Categories.AsNoTracking().AsQueryable();



        var result = await comm
            .OrderBy(request.Sort)
            .ProjectTo<CategoryListDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<CategoryListDto>>.Success(result);
    }
}



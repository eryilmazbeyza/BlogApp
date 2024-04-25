using AutoMapper;
using AutoMapper.QueryableExtensions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Blog.Application.Features.Comment.Queries.CommentListWithPagination;

public record CommentListWithPaginationQuery : IRequest<Result<PaginatedList<CommentListDto>>>
{
    public int PageSize { get; init; } = 50;
    public int PageNumber { get; init; } = 1;
    public string Sort { get; init; } = "Id desc";

    public long? PostId { get; init; }

}

internal sealed class CommentListWithPaginationQueryHandler : IRequestHandler<CommentListWithPaginationQuery, Result<PaginatedList<CommentListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public CommentListWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<CommentListDto>>> Handle(CommentListWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var comm = _context.Comments.AsNoTracking().AsQueryable();

        if (request.PostId > 0)
            comm = comm.Where(x => x.Post.Id == request.PostId);


        var result = await comm
            .OrderBy(request.Sort)
            .ProjectTo<CommentListDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<CommentListDto>>.Success(result);
    }
}

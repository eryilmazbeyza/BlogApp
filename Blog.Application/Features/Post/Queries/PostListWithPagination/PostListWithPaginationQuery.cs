using AutoMapper;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;


namespace Blog.Application.Features.Post.Queries.PostListWithPagination;

public record PostListWithPaginationQuery : IRequest<Result<PaginatedList<PostListDto>>>
{
    public int PageSize { get; init; } = 50;
    public int PageNumber { get; init; } = 1;
    public string Sort { get; init; } = "Id desc";
}

internal sealed class PostListWithPaginationQueryHandler : IRequestHandler<PostListWithPaginationQuery, Result<PaginatedList<PostListDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public PostListWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaginatedList<PostListDto>>> Handle(PostListWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var posts = _context.Post


        var userList = _userService.GetUserList();
        var result = await userList
            .OrderBy(request.Sort)
            .ProjectTo<PostListDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return Result<PaginatedList<PostListDto>>.Success(result);
    }
}

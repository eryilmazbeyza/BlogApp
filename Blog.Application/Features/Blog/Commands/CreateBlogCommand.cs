using Blog.Application.Common.Context;
using Blog.Application.Common.Models;
using MediatR;

namespace Blog.Application.Features.Blog.Commands;

public record CreateBlogCommand : IRequest<Result<long>>
{
    public string Title { get; init; } = null!;
}

internal sealed class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<long>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var post = new Domain.Entities.Post
        {
            Title = request.Title
        };

        await _context.Posts.AddAsync(post, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<long>.Success(post.Id, ResultMessages.Success);

    }
}
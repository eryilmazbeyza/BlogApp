using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Post.Commands.Create;

public record CreatePostCommand : IRequest<Result<long>>
{
    public string Title { get; init; } = null!;
    public string? Content { get; init; }
    public string? Author { get; init; }
    public string? ImageUrl { get; init; }
    public int? Rating { get; init; }
    public long? CategoryId { get; init; }
}

internal sealed class CreateBlogCommandHandler : IRequestHandler<CreatePostCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<long>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Domain.Entities.Post
        {
            Title = request.Title,
            Content = request.Content,
            Author = request.Author,
            ImageUrl = request.ImageUrl,
            Rating = request.Rating,
        };


        if (request.CategoryId > 0)
        {
            post.Category = _context.Categories.FirstOrDefault(x => x.Id == request.CategoryId);
        }

        await _context.Posts.AddAsync(post, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<long>.Success(post.Id, ResultMessages.Success);
    }
}
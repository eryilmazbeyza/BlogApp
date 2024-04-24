using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Post.Commands.Update;

public record UpdatePostCommand : IRequest<Result>
{
    public long Id { get; init; }
    public string? Title { get; init; }
    public string? Content { get; init; }
    public string? Author { get; init; }
    public string? ImageUrl { get; init; }
    public int? Rating { get; init; }
    public long? CategoryId { get; init; }
}

internal sealed class CreateBlogCommandHandler : IRequestHandler<UpdatePostCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (post is null)
            return Result.Failure("Post bulunamadı");

        if(!string.IsNullOrEmpty(request.Title))
            post.Title = request.Title;

        if(!string.IsNullOrEmpty(request.Content)) 
            post.Content = request.Content;

        if (!string.IsNullOrEmpty(request.Author))
            post.Author = request.Author;

        if (!string.IsNullOrEmpty(request.ImageUrl))
            post.ImageUrl = request.ImageUrl;

        if (request.Rating > 0)
            post.Rating = request.Rating;

        if (request.CategoryId > 0)
            post.Category = _context.Categories.FirstOrDefault(x => x.Id == request.CategoryId);

        _context.Posts.Update(post);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(ResultMessages.Success);
    }
}
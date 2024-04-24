using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Post.Commands.Delete;

public record DeletePostCommand(long Id) : IRequest<Result>;

internal sealed class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeletePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _context.Posts
          .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (post is null)
            return Result.Failure("Post bulunamadı");

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(ResultMessages.Success);
    }
}

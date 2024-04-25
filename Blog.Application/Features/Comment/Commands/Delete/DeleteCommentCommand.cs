using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Comment.Commands.Delete;

public record DeleteCommentCommand(long Id) : IRequest<Result>;

internal sealed class DeletePostCommandHandler : IRequestHandler<DeleteCommentCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeletePostCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var comm = await _context.Comments
          .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (comm is null)
            return Result.Failure("Comment bulunamadı");

        _context.Comments.Remove(comm);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(ResultMessages.Success);
    }
}


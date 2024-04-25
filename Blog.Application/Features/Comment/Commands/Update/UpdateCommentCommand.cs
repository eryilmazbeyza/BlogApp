using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.Comment.Commands.Update;

public record UpdateCommentCommand : IRequest<Result>
{
    public long Id { get; init; }
    public string? Content { get; init; }
    public long? PostId { get; init; }
}

internal sealed class CreateBlogCommandHandler : IRequestHandler<UpdateCommentCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var comm = await _context.Comments
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (comm is null)
            return Result.Failure("Comment bulunamadı");

        if (!string.IsNullOrEmpty(request.Content))
            comm.Content = request.Content;

        if (request.PostId > 0)
            comm.Post = _context.Posts.FirstOrDefault(x => x.Id == request.PostId);

        _context.Comments.Update(comm);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(ResultMessages.Success);
    }
}

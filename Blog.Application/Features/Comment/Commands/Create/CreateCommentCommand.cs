using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;


namespace Blog.Application.Features.Comment.Commands.Create;


public record CreateCommentCommand : IRequest<Result<long>>
{
    public string? Content { get; init; }
    public long? PostId { get; init; }
}

internal sealed class CreateBlogCommandHandler : IRequestHandler<CreateCommentCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;

    public CreateBlogCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<long>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var comm = new Domain.Entities.Comment
        {
            
            Content = request.Content,
           
        };


        if (request.PostId > 0)
        {
            comm.Post = _context.Posts.FirstOrDefault(x => x.Id == request.PostId);
        }

        await _context.Comments.AddAsync(comm, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<long>.Success(comm.Id, ResultMessages.Success);
    }
}

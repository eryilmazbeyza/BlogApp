using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;

namespace Blog.Application.Features.Category.Commands.Create;

public record CreateCategoryCommand : IRequest<Result<long>>
{
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
}

internal sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<long>>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<long>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = new Domain.Entities.Category
        {

            Title = request.Title,
            Description = request.Description,

        };



        await _context.Categories.AddAsync(cat, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<long>.Success(cat.Id, ResultMessages.Success);
    }
}

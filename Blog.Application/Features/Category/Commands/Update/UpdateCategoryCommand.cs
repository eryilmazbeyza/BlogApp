using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Features.Category.Commands.Update;

public record UpdateCategoryCommand : IRequest<Result>
{
    public long Id { get; init; }
    public string Title { get; init; } = null!;
    public string? Description { get; init; }
}

internal sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public UpdateCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = await _context.Categories
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (cat is null)
            return Result.Failure("Category bulunamadı");

        if (!string.IsNullOrEmpty(request.Title))
            cat.Title = request.Title;

        if (!string.IsNullOrEmpty(request.Description))
            cat.Description = request.Description;

        _context.Categories.Update(cat);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(ResultMessages.Success);
    }
}

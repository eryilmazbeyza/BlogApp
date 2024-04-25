using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Features.Category.Commands.Delete;

public record DeleteCategoryCommand(long Id) : IRequest<Result>;

internal sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteCategoryCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var cat = await _context.Categories
          .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (cat is null)
            return Result.Failure("Category bulunamadı");

        _context.Categories.Remove(cat);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(ResultMessages.Success);
    }
}

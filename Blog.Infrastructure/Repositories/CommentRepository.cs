using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CommentRepository(ApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }
    public async Task<CommentClass> CreateAsync(CommentClass commentClass)
    {
        await _applicationDbContext.CommentClasses.AddAsync(commentClass);
        await _applicationDbContext.SaveChangesAsync();
        return commentClass;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _applicationDbContext.CommentClasses
             .Where(model => model.CommentId == id)
             .ExecuteDeleteAsync();
    }

    public async Task<List<CommentClass>> GetAllAsync()
    {
        return await _applicationDbContext.CommentClasses.ToListAsync();
    }

    public async Task<CommentClass> GetByIdAsync(int id)
    {
        return await _applicationDbContext.CommentClasses.AsNoTracking()
   .FirstOrDefaultAsync(b => b.CommentId == id);
    }

    public async Task<int> UpdateAsync(int id, CommentClass commentClass)
    {
        return await _applicationDbContext.CommentClasses
    .Where(model => model.CommentId == id)
    .ExecuteUpdateAsync(setters => setters
    .SetProperty(m => m.Content, commentClass.Content)
    );
    }
}

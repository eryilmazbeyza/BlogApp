using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PostRepository(ApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }
    public async Task<PostClass> CreateAsync(PostClass postClass)
    {
        await _applicationDbContext.PostClasses.AddAsync(postClass);
        await _applicationDbContext.SaveChangesAsync();
        return postClass;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _applicationDbContext.PostClasses
             .Where(model => model.PostId == id)
             .ExecuteDeleteAsync();
    }

    public async Task<List<PostClass>> GetAllAsync()
    {
        return await _applicationDbContext.PostClasses.ToListAsync();
    }

    public async Task<PostClass> GetByIdAsync(int id)
    {
        return await _applicationDbContext.PostClasses.AsNoTracking()
    .FirstOrDefaultAsync(b => b.PostId == id);
    }

    public async Task<int> UpdateAsync(int id, PostClass postClass)
    {
        return await _applicationDbContext.PostClasses
    .Where(model => model.PostId == id)
    .ExecuteUpdateAsync(setters => setters
    .SetProperty(m => m.Title, postClass.Title)
    .SetProperty(m => m.Content, postClass.Content)
    .SetProperty(m => m.Author, postClass.Author)
    .SetProperty(m => m.ImageUrl, postClass.ImageUrl)
    .SetProperty(m => m.Rating, postClass.Rating)
    );
    }
}

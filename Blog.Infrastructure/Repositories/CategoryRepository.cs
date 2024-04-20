using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public CategoryRepository(ApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }
    public async Task<CategoryClass> CreateAsync(CategoryClass category)
    {
        await _applicationDbContext.CategoryClasses.AddAsync(category);
        await _applicationDbContext.SaveChangesAsync();
        return category;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _applicationDbContext.CategoryClasses
             .Where(model => model.CategoryId == id)
             .ExecuteDeleteAsync();
    }

    public async Task<List<CategoryClass>> GetAllAsync()
    {
        return await _applicationDbContext.CategoryClasses.ToListAsync();
    }

    public async Task<CategoryClass> GetByIdAsync(int id)
    {
        return await _applicationDbContext.CategoryClasses.AsNoTracking()
    .FirstOrDefaultAsync(b => b.CategoryId == id);
    }

    public async Task<int> UpdateAsync(int id, CategoryClass category)
    {
        return await _applicationDbContext.CategoryClasses
              .Where(model => model.CategoryId == id)
              .ExecuteUpdateAsync(setters => setters
              .SetProperty(m => m.Name, category.Name)
              .SetProperty(m => m.Description, category.Description)
        );
    }
}

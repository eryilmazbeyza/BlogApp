using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces;

public interface ICategoryRepository
{
    Task<List<CategoryClass>> GetAllAsync();
    Task<CategoryClass> GetByIdAsync(int id);
    Task<CategoryClass> CreateAsync(CategoryClass category);
    Task<int> UpdateAsync(int id, CategoryClass category);
    Task<int> DeleteAsync(int id);
}

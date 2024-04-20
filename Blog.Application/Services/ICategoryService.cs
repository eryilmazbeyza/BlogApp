using Blog.Domain.Entities;

namespace Blog.Application.Services;

public interface ICategoryService
{
    Task<List<CategoryClass>> GetAllAsync();
    Task<CategoryClass> GetByIdAsync(int id);
    Task<CategoryClass> CreateAsync(CategoryClass categoryClass);
    Task<int> UpdateAsync(int id, CategoryClass categoryClass);
    Task<int> DeleteAsync(int id);
}

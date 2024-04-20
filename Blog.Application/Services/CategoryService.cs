using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        this._categoryRepository = categoryRepository;
    }
    public async Task<CategoryClass> CreateAsync(CategoryClass categoryClass)
    {
        return await _categoryRepository.CreateAsync(categoryClass);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _categoryRepository.DeleteAsync(id);
    }

    public async Task<List<CategoryClass>> GetAllAsync()
    {
        return await _categoryRepository.GetAllAsync();
    }

    public async Task<CategoryClass> GetByIdAsync(int id)
    {
        return await _categoryRepository.GetByIdAsync(id);
    }

    public async Task<int> UpdateAsync(int id, CategoryClass categoryClass)
    {
        return await _categoryRepository.UpdateAsync(id, categoryClass);
    }
}

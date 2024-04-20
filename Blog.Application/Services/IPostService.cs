using Blog.Domain.Entities;

namespace Blog.Application.Services;

public interface IPostService
{
    Task<List<PostClass>> GetAllAsync();
    Task<PostClass> GetByIdAsync(int id);
    Task<PostClass> CreateAsync(PostClass postClass);
    Task<int> UpdateAsync(int id, PostClass postClass);
    Task<int> DeleteAsync(int id);
}

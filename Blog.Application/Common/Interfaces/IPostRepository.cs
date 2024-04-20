using Blog.Domain.Entities;

namespace Blog.Application.Common.Interfaces;

public interface IPostRepository
{
    Task<List<PostClass>> GetAllAsync();
    Task<PostClass> GetByIdAsync(int id);
    Task<PostClass> CreateAsync(PostClass postClass);
    Task<int> UpdateAsync(int id, PostClass postClass);
    Task<int> DeleteAsync(int id);
}

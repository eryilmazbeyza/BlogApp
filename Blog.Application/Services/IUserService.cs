using Blog.Domain.Entities;

namespace Blog.Application.Services;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> CreateAsync(User user);
    Task<int> UpdateAsync(int id, User user);
    Task<int> DeleteAsync(int id);
}

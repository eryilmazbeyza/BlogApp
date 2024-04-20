using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        this._userRepository = userRepository;
    }
    public async Task<User> CreateAsync(User user)
    {
        return await _userRepository.CreateAsync(user);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<int> UpdateAsync(int id, User user)
    {
        return await _userRepository.UpdateAsync(id, user);
    }
}

using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Blog.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        this._applicationDbContext = applicationDbContext;
    }
    public async Task<User> CreateAsync(User user)
    {
        await _applicationDbContext.Users.AddAsync(user);
        await _applicationDbContext.SaveChangesAsync();
        return user;
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _applicationDbContext.Users
            .Where(model => model.UserId == id)
            .ExecuteDeleteAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _applicationDbContext.Users.ToListAsync();
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _applicationDbContext.Users.AsNoTracking()
    .FirstOrDefaultAsync(b => b.UserId == id);
    }

    public async Task<int> UpdateAsync(int id, User user)
    {
        return await _applicationDbContext.Users
    .Where(model => model.UserId == id)
    .ExecuteUpdateAsync(setters => setters
    .SetProperty(m => m.FirstName, user.FirstName)
    .SetProperty(m => m.LastName, user.LastName)
    .SetProperty(m => m.Username, user.Username)
    .SetProperty(m => m.Email, user.Email)
    .SetProperty(m => m.Password, user.Password)
    );
    }
}

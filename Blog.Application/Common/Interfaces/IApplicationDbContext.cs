using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<CategoryClass> CategoryClasses { get; }
    DbSet<PostClass> PostClasses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

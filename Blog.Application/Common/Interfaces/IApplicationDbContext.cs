using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Category> CategoryClasses { get; }
    DbSet<Post> PostClasses { get; }
    DbSet<Comment> CommentClasses { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

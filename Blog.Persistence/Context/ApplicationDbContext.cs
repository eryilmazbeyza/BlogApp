using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blog.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<CategoryClass> CategoryClasses => Set<CategoryClass>();

    public DbSet<PostClass> PostClasses => Set<PostClass>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

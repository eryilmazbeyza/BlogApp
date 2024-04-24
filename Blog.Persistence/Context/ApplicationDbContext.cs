using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using Blog.Application.Common.Context;
using Blog.Persistence.Inceptors;
using Blog.Persistence.Configuration;

namespace Blog.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Post> Posts => Set<Post>();



    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces().Any(i => i == typeof(IAppConfiguration)));
        base.OnModelCreating(builder);

        // otomatik gelen requestlere isdeleted, family, subfamilyId ekleniyor.
        Expression<Func<BaseAuditableEntity, bool>> filterExpr = bm => !bm.IsDeleted;
        foreach (var mutableEntityType in builder.Model.GetEntityTypes())
        {
            var parameter = Expression.Parameter(mutableEntityType.ClrType);
            Expression? body = null;

            // Gelen objenin tipine göre filtre uygulanan yerdir. Farklı bir class tipi için bir query uygulanmak istenirse buraya eklenmelidir
            if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseAuditableEntity)))
            {
                body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
            }

            if (body != null)
            {
                var lambdaExpression = Expression.Lambda(body, parameter);
                mutableEntityType.SetQueryFilter(lambdaExpression);
            }
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
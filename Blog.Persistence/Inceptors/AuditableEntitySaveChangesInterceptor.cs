using Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Blog.Application.Common.Interfaces;

namespace Blog.Persistence.Inceptors;

public class AuditableEntitySaveChangesInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public AuditableEntitySaveChangesInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);
        try
        {
            return base.SavingChanges(eventData, result);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new Exception("Eşzamanlı bir güncelleme işlemi yapılamaz!");
        }
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);
        try
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new Exception("Eşzamanlı bir güncelleme işlemi yapılamaz!");
        }
    }

    public void UpdateEntities(DbContext? context)
    {
        if (context is null) return;

        var entriesToModify = context.ChangeTracker.Entries<BaseAuditableEntity>().ToArray();
        if (entriesToModify is null) return;

        foreach (var entry in entriesToModify)
        {
            if (entry.State == EntityState.Added || entry.HasChangedOwnedEntitiesAdded())
            {
                entry.Entity.Created = DateTime.Now;
                entry.Entity.CreatedBy = _currentUserService.UserId;
            }

            if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntitiesModified())
            {
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModified = DateTime.Now;

                if (entry.Entity.IsHardDelete)
                {
                    entry.State = EntityState.Deleted;
                }
            }

            if (entry.State == EntityState.Deleted && !entry.Entity.IsHardDelete)
            {
                entry.Entity.DeletedBy = _currentUserService.UserId;
                entry.Entity.Deleted = DateTime.Now;
                entry.State = EntityState.Modified;
            }
        }
    }
}

public static class Extensions
{
    public static bool HasChangedOwnedEntitiesAdded(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added));

    public static bool HasChangedOwnedEntitiesModified(this EntityEntry entry) =>
        entry.References.Any(r =>
            r.TargetEntry != null &&
            r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Modified));
}
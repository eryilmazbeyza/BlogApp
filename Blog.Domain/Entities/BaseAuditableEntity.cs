namespace Blog.Domain.Entities;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime? Created { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public long? LastModifiedBy { get; set; }
    public DateTime? Deleted { get; set; }
    public long? DeletedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
}

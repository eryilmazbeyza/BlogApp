
namespace Blog.Domain.Entities;

public class Category : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}


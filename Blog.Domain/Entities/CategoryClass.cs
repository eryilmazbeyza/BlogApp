using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Domain.Entities;

public class CategoryClass : BaseAuditableEntity
{
    [Key]
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}


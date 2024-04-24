using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Domain.Entities;

public class Comment : BaseAuditableEntity
{
    public string? Content { get; set; }
    public Post? Post { get; set; } = null!;
}

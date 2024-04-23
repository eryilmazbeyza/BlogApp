using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Domain.Entities;

public class CommentClass : BaseAuditableEntity
{
    [Key]
    public int CommentId { get; set; }
    public string? Content { get; set; }

    [ForeignKey("PostId")]
    public PostClass? PostClass { get; set; } = null!;
}

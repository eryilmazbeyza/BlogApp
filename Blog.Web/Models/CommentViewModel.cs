using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models;

public class CommentViewModel :  BaseAuditableEntityViewModel
{
    [Key]
    public int CommentId { get; set; }
    public string? Content { get; set; }

    [ForeignKey("PostId")]
    public PostViewModel? PostClass { get; set; } = null!;
}

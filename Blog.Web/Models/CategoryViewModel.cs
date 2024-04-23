using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models;

public class CategoryViewModel: BaseAuditableEntityViewModel
{
    [Key]
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    [ForeignKey("PostId")]
    public PostViewModel? PostClass { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models;

public class PostViewModel : BaseAuditableEntityViewModel
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }
    public int? Rating { get; set; }

    public CategoryViewModel? Category { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }
    public int? Rating { get; set; }
    public Category? Category { get; set; } = null!;
}

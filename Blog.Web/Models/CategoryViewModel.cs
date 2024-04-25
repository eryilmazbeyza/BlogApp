using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Models;

public class CategoryViewModel: BaseAuditableEntityViewModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }

}

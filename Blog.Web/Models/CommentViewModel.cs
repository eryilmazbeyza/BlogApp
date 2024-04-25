using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Models;

public class CommentViewModel :  BaseAuditableEntityViewModel
{
    public string? Content { get; set; }
    public PostViewModel? Post { get; set; }
}

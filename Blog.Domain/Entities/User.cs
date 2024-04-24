using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Firstname { get; set; } = null!;
    public string Lastname { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities;

public class User:BaseAuditableEntity
{
    [Key]
    public int UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

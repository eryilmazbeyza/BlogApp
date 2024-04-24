
namespace Blog.Domain.Entities;

public abstract class BaseEntity
{
    public long Id { get; set; }

    /// <summary>
    /// true ise kalıcı olarak databaseden dataları siler
    /// </summary>
    public bool IsHardDelete { get; set; } = false;
}
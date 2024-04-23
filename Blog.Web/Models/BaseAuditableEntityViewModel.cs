using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Web.Models;

public class BaseAuditableEntityViewModel
{
    
    public DateTimeOffset? Created { get; set; }
    
    public string? CreatedBy { get; set; }
    
    public DateTimeOffset? LastModified { get; set; }
    
    public string? LastModifiedBy { get; set; }

    private readonly List<BaseEventViewModel> _domainEvents = new();

    [NotMapped]
    public IReadOnlyCollection<BaseEventViewModel> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEventViewModel domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEventViewModel domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}

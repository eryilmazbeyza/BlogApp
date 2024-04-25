using Blog.Application.Common.Mappings;

namespace Blog.Application.Features.Category.Queries;

public class CategoryListDto : IMapFrom<Domain.Entities.Category>
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
}
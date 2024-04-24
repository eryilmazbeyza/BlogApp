using Blog.Application.Common.Mappings;


namespace Blog.Application.Features.Post.Queries.PostListWithPagination;

public class PostListDto : IMapFrom<Domain.Entities.Post>
{
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }
    public int? Rating { get; set; }
}

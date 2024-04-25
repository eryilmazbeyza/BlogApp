using Blog.Application.Common.Mappings;

namespace Blog.Application.Features.Comment.Queries.CommentListWithPagination;

public class CommentListDto : IMapFrom<Domain.Entities.Comment>
{
    public long Id { get; set; }
    public string? Content { get; set; }
    public PostDto? Post { get; set; }
}

public class PostDto : IMapFrom<Domain.Entities.Post>
{
    public long Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Content { get; set; }
    public string? Author { get; set; }
    public string? ImageUrl { get; set; }
    public int? Rating { get; set; }
}

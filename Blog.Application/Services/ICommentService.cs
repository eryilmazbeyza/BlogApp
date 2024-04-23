using Blog.Domain.Entities;

namespace Blog.Application.Services;

public interface ICommentService
{
    Task<List<CommentClass>> GetAllAsync();
    Task<CommentClass> GetByIdAsync(int id);
    Task<CommentClass> CreateAsync(CommentClass commentClass);
    Task<int> UpdateAsync(int id, CommentClass commentClass);
    Task<int> DeleteAsync(int id);
}

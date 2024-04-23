using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        this._commentRepository = commentRepository;
    }
    public async Task<CommentClass> CreateAsync(CommentClass commentClass)
    {
        return await _commentRepository.CreateAsync(commentClass);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _commentRepository.DeleteAsync(id);
    }

    public async Task<List<CommentClass>> GetAllAsync()
    {
        return await _commentRepository.GetAllAsync();
    }

    public async Task<CommentClass> GetByIdAsync(int id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }

    public async Task<int> UpdateAsync(int id, CommentClass commentClass)
    {
        return await _commentRepository.UpdateAsync(id, commentClass);
    }
}

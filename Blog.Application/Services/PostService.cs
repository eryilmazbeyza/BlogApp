using Blog.Application.Common.Interfaces;
using Blog.Domain.Entities;

namespace Blog.Application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        this._postRepository = postRepository;
    }
    public async Task<PostClass> CreateAsync(PostClass postClass)
    {
        return await _postRepository.CreateAsync(postClass);
    }

    public async Task<int> DeleteAsync(int id)
    {
        return await _postRepository.DeleteAsync(id);
    }

    public async Task<List<PostClass>> GetAllAsync()
    {
        return await _postRepository.GetAllAsync();
    }

    public async Task<PostClass> GetByIdAsync(int id)
    {
        return await _postRepository.GetByIdAsync(id);
    }

    public async Task<int> UpdateAsync(int id, PostClass postClass)
    {
        return await _postRepository.UpdateAsync(id, postClass);
    }
}

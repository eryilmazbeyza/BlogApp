using Blog.Application.Services;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        this._postService = postService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var posts = await _postService.GetAllAsync();
        return Ok(posts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var post = await _postService.GetByIdAsync(id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PostClass post)
    {
        var createdPost = await _postService.CreateAsync(post);
        return CreatedAtAction(nameof(GetById), new { id = createdPost.PostId }, createdPost);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, PostClass updatedPost)
    {
        int existingPost = await _postService.UpdateAsync(id, updatedPost);
        if (existingPost == 0)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int post = await _postService.DeleteAsync(id);
        if (post == 0)
        {
            return BadRequest();
        }
        return NoContent();
    }
}

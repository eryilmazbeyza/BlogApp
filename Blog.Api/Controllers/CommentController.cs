using Blog.Application.Services;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        this._commentService = commentService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comms = await _commentService.GetAllAsync();
        return Ok(comms);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comm = await _commentService.GetByIdAsync(id);
        if (comm == null)
        {
            return NotFound();
        }
        return Ok(comm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CommentClass comment)
    {
        var createdComment = await _commentService.CreateAsync(comment);
        return CreatedAtAction(nameof(GetById), new { id = createdComment.CommentId }, createdComment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CommentClass updatedComment)
    {
        int existingComment = await _commentService.UpdateAsync(id, updatedComment);
        if (existingComment == 0)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int comm = await _commentService.DeleteAsync(id);
        if (comm == 0)
        {
            return BadRequest();
        }
        return NoContent();
    }
}

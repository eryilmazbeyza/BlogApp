using Blog.Application.Services;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        this._categoryService = categoryService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var categorie = await _categoryService.GetByIdAsync(id);
        if (categorie == null)
        {
            return NotFound();
        }
        return Ok(categorie);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryClass category)
    {
        var createdCategorie = await _categoryService.CreateAsync(category);
        return CreatedAtAction(nameof(GetById), new { id = createdCategorie.CategoryId }, createdCategorie);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CategoryClass updatedCategory)
    {
        int existingCategory = await _categoryService.UpdateAsync(id, updatedCategory);
        if (existingCategory == 0)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        int category = await _categoryService.DeleteAsync(id);
        if (category == 0)
        {
            return BadRequest();
        }
        return NoContent();
    }
}

using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class CategoryController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string searchBy, string search)
    {
        List<CategoryViewModel> cats = new List<CategoryViewModel>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");

            HttpResponseMessage response = await client.GetAsync($"Category?searchBy={searchBy}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                cats = await response.Content.ReadAsAsync<List<CategoryViewModel>>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occurred");
            }
        }

        if (searchBy == "Name")
        {
            cats = cats.Where(x => x.Name == search || search == null).ToList();
        }

        return View(cats);
    }

    [HttpGet]
    public async Task<IActionResult> CategoryList()
    {
        List<CategoryViewModel> cats = new List<CategoryViewModel>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.GetAsync("Category");
            if (response.IsSuccessStatusCode)
            {
                cats = await response.Content.ReadAsAsync<List<CategoryViewModel>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occured");
            }
        }
        return View(cats);
    }

    [HttpGet]
    public IActionResult AddCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddCategory(CategoryViewModel categoryViewModel)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.PostAsJsonAsync("Category", categoryViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category");
            }
        }
        ModelState.AddModelError(string.Empty, "Error Occurred");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditCategory(int id)
    {
        CategoryViewModel categoryViewModel = new CategoryViewModel();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.GetAsync("Category/" + id);
            if (response.IsSuccessStatusCode)
            {
                categoryViewModel = await response.Content.ReadAsAsync<CategoryViewModel>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occured");
            }
        }
        return View(categoryViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(CategoryViewModel categoryViewModel)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.PutAsJsonAsync("Category/" + categoryViewModel.CategoryId, categoryViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category");
            }
        }
        ModelState.AddModelError(string.Empty, "Error Occurred");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.DeleteAsync("Category/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Category");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occured");
            }
        }
        return View("Index", new List<CategoryViewModel>());
    }
}

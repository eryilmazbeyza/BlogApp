using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace Blog.Web.Controllers;

public class CommentController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string searchBy, string search)
    {
        List<CommentViewModel> comms = new List<CommentViewModel>();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");

            HttpResponseMessage response = await client.GetAsync($"Comment?searchBy={searchBy}&search={search}");

            if (response.IsSuccessStatusCode)
            {
                comms = await response.Content.ReadAsAsync<List<CommentViewModel>>();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occurred");
            }
        }

        if (searchBy == "Content")
        {
            comms = comms.Where(x => x.Content == search || search == null).ToList();
        }

        return View(comms);
    }
    [HttpGet]
    public async Task<IActionResult> CommentList()
    {
        List<CommentViewModel> comms = new List<CommentViewModel>();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.GetAsync("Comment");
            if (response.IsSuccessStatusCode)
            {
                comms = await response.Content.ReadAsAsync<List<CommentViewModel>>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occured");
            }
        }
        return View(comms);
    }

    [HttpGet]
    public IActionResult AddComment()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddComment(CommentViewModel commentViewModel)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.PostAsJsonAsync("Comment", commentViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment");
            }
        }
        ModelState.AddModelError(string.Empty, "Error Occurred");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> EditComment(int id)
    {
        CommentViewModel commentViewModel = new CommentViewModel();

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.GetAsync("Comment/" + id);
            if (response.IsSuccessStatusCode)
            {
                commentViewModel = await response.Content.ReadAsAsync<CommentViewModel>();

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occured");
            }
        }
        return View(commentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditComment(CommentViewModel commentViewModel)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.PutAsJsonAsync("Comment/" + commentViewModel.CommentId, commentViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment");
            }
        }
        ModelState.AddModelError(string.Empty, "Error Occurred");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteComment(int id)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.DeleteAsync("Comment/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Comment");

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error Occured");
            }
        }
        return View("Index", new List<CommentViewModel>());
    }
}

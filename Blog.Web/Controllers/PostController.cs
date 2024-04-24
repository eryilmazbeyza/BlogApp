using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class PostController : Controller
{

    //[HttpGet]
    //public async Task<IActionResult> Index(string searchBy, string search)
    //{
    //    List<PostViewModel> posts = new List<PostViewModel>();

    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");

    //        HttpResponseMessage response = await client.GetAsync($"Post?searchBy={searchBy}&search={search}");

    //        if (response.IsSuccessStatusCode)
    //        {
    //            posts = await response.Content.ReadAsAsync<List<PostViewModel>>();
    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Error Occurred");
    //        }
    //    }

    //    if (searchBy == "Title")
    //    {
    //        posts = posts.Where(x => x.Title == search || search == null).ToList();
    //    }
    //    else
    //    {
    //        posts = posts.Where(x => x.Content == search || search == null).ToList();
    //    }

    //    return View(posts);
    //}

    //[HttpGet]
    //public async Task<IActionResult> PostList()
    //{
    //    List<PostViewModel> posts = new List<PostViewModel>();
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.GetAsync("Post");
    //        if (response.IsSuccessStatusCode)
    //        {
    //            posts = await response.Content.ReadAsAsync<List<PostViewModel>>();

    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Error Occured");
    //        }
    //    }
    //    return View(posts);
    //}

    //[HttpGet]
    //public IActionResult AddPost()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> AddPost(PostViewModel postViewModel)
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.PostAsJsonAsync("Post", postViewModel);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            return RedirectToAction("Index", "Post");
    //        }
    //    }
    //    ModelState.AddModelError(string.Empty, "Error Occurred");
    //    return View();
    //}

    //[HttpGet]
    //public async Task<IActionResult> EditPost(int id)
    //{
    //    PostViewModel postViewModel = new PostViewModel();

    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.GetAsync("Post/" + id);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            postViewModel = await response.Content.ReadAsAsync<PostViewModel>();

    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Error Occured");
    //        }
    //    }
    //    return View(postViewModel);
    //}

    //[HttpPost]
    //public async Task<IActionResult> EditPost(PostViewModel postViewModel)
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.PutAsJsonAsync("Post/" + postViewModel.PostId, postViewModel);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            return RedirectToAction("Index", "Post");
    //        }
    //    }
    //    ModelState.AddModelError(string.Empty, "Error Occurred");
    //    return View();
    //}

    //[HttpGet]
    //public async Task<IActionResult> DeletePost(int id)
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.DeleteAsync("Post/" + id);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            return RedirectToAction("Index", "Post");

    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Error Occured");
    //        }
    //    }
    //    return View("Index", new List<PostViewModel>());
    //}
}

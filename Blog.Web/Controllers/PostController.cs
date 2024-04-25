using Blog.Web.HttpClient;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Web.Controllers;

public class PostController : Controller
{
    private readonly IHttpClientWrapper _httpClient;

    public PostController(IHttpClientWrapper httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            // Tüm postları getirmek için bir HTTP GET isteği gönderilir
            var postsResponse = await _httpClient.GetAsync<List<PostViewModel>>("Post", "your_token_here", null); // "your_token_here" yerine geçerli bir token eklemelisiniz

            if (!postsResponse.isSuccess)
            {
                // Postlar getirilemediyse uygun bir hata işlemi gerçekleştirilebilir
                ModelState.AddModelError(string.Empty, "Error Occurred");
                return View("Error");
            }

            // Post listesi görüntülenir
            return View(postsResponse.Data);
        }
        catch (Exception ex)
        {
            // Exception yakalanırsa uygun bir hata işlemi gerçekleştirilebilir
            ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult AddPost()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPost(PostViewModel postViewModel)
    {
        var serializedModel = JsonConvert.SerializeObject(postViewModel);

        var response = await _httpClient.PostAsync<long>("Post", "your_token_here", serializedModel); // "your_token_here" yerine geçerli bir token eklemelisiniz
        if (!response.isSuccess)
            ModelState.AddModelError(string.Empty, "Error Occurred");

        // Eğer Post ekleme başarılı ise, kullanıcıyı Post listesine yönlendir
        return RedirectToAction("Index", "Post");
    }

    [HttpGet]
    public async Task<IActionResult> UpdatePost(int id)
    {
        var postResponse = await _httpClient.GetAsync<PostViewModel>($"Post/{id}", "your_token_here", null); // "your_token_here" yerine geçerli bir token eklemelisiniz

        if (!postResponse.isSuccess)
        {
            ModelState.AddModelError(string.Empty, "Post not found");
            return View("Error");
        }

        return View(postResponse.Data);
    }

    [HttpPost]
    public async Task<IActionResult> UpdatePost(PostViewModel postViewModel)
    {
        var serializedModel = JsonConvert.SerializeObject(postViewModel);

        var response = await _httpClient.PutAsync<PostViewModel>("Post", "your_token_here", serializedModel); // "your_token_here" yerine geçerli bir token eklemelisiniz
        if (!response.isSuccess)
        {
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View(postViewModel);
        }

        return RedirectToAction("Index", "Post");
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost(int id)
    {
        var response = await _httpClient.DeleteAsync($"Post/{id}", "your_token_here"); // "your_token_here" yerine geçerli bir token eklemelisiniz

        if (!response.isSuccess)
        {
            ModelState.AddModelError(string.Empty, "Error Occurred");
        }

        return RedirectToAction("Index", "Post");
    }
}

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


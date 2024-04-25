using Blog.Web.HttpClient;
using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Blog.Web.Controllers;

public class UserController : Controller
{
    private readonly IHttpClientWrapper _httpClient;

    public UserController(IHttpClientWrapper httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(UserViewModel userViewModel)
    {
        var serializedModel = JsonConvert.SerializeObject(userViewModel);

        var response = await _httpClient.PostAsync<long>("Users", "", serializedModel);
        if(!response.isSuccess)
            ModelState.AddModelError(string.Empty, "Error Occurred");


        //login sayfasına yönlenecek
        return RedirectToAction("Index", "User");
    }

    [HttpGet]
    public async Task<IActionResult> UpdateUser(int id)
    {
        // Kullanıcının güncellenmek istendiği bilgileri almak için HTTP GET isteği gönderilir
        var userResponse = await _httpClient.GetAsync<UserViewModel>($"Users/{id}", "your_token_here", null); // "your_token_here" yerine geçerli bir token eklemelisiniz

        if (!userResponse.isSuccess)
        {
            // Kullanıcı bulunamadıysa uygun bir hata işlemi gerçekleştirilebilir
            ModelState.AddModelError(string.Empty, "User not found");
            return View("Error");
        }

        // Kullanıcı bilgileri güncelleme formuna gönderilir
        return View(userResponse.Data);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateUser(UserViewModel userViewModel)
    {
        var serializedModel = JsonConvert.SerializeObject(userViewModel);

        var response = await _httpClient.PutAsync<UserViewModel>("Users", "your_token_here", serializedModel); // "your_token_here" yerine geçerli bir token eklemelisiniz
        if (!response.isSuccess)
        {
            ModelState.AddModelError(string.Empty, "Error Occurred");
            return View(userViewModel);
        }

        return RedirectToAction("Index", "User");
    }

    //[HttpGet]
    //public async Task<IActionResult> Index()
    //{
    //    List<UserViewModel> users = new List<UserViewModel>();
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.GetAsync("User");
    //        if (response.IsSuccessStatusCode)
    //        {
    //            users = await response.Content.ReadAsAsync<List<UserViewModel>>();

    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Error Occured");
    //        }
    //    }
    //    return View(users);
    //}



    //[HttpGet]
    //public IActionResult UserRegistration()
    //{
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> UserRegistration(UserViewModel userViewModel)
    //{
    //    using (var client = new HttpClient())
    //    {
    //        client.BaseAddress = new Uri("https://localhost:7002/api/");
    //        HttpResponseMessage response = await client.PostAsJsonAsync("User", userViewModel);
    //        if (response.IsSuccessStatusCode)
    //        {
    //            return RedirectToAction("Index", "User");
    //        }
    //    }
    //    ModelState.AddModelError(string.Empty, "Error Occurred");
    //    return View();
    //}
}

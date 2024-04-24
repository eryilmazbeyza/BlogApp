using Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers;

public class UserController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
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
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7002/api/");
            HttpResponseMessage response = await client.PostAsJsonAsync("User", userViewModel);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User");
            }
        }
        ModelState.AddModelError(string.Empty, "Error Occurred");
        return View();
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

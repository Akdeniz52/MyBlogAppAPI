using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MyBlogAppClient.Models;

namespace MyBlogAppClient.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model, IFormFile Image)
        {
            if (!ModelState.IsValid)
                return View(model);

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5261/");

            using var content = new MultipartFormDataContent();
            content.Add(new StringContent(model.UserName ?? ""), "UserName");
            content.Add(new StringContent(model.FullName ?? ""), "FullName");
            content.Add(new StringContent(model.Email ?? ""), "Email");
            content.Add(new StringContent(model.Password ?? ""), "Password");
            content.Add(new StringContent(model.ConfirmPassword ?? ""), "ConfirmPassword");

            if (Image != null && Image.Length > 0)
            {
                var streamContent = new StreamContent(Image.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(Image.ContentType);
                content.Add(streamContent, "Image", Image.FileName);
            }

            var response = await client.PostAsync("api/user/register", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Login");
            }

            ModelState.AddModelError(string.Empty, "Kayıt başarısız.");
            return View(model);
        }

        [HttpGet]
        public IActionResult Profile(string username)
        {
            ViewBag.Username = username;
            return View();
        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult AdminPanelPost()
        {
            return View();
        }

    }
}
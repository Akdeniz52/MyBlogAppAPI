
using Microsoft.AspNetCore.Mvc;


namespace MyBlogAppClient.Controllers;

public class HomeController : Controller
{
    

    public IActionResult Index()
    {
        return RedirectToAction("Posts", "Post");
    }


    
}

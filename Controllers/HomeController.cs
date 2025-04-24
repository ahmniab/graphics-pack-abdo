using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Graphics.Models;

namespace Graphics.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string url="/Shapes")
    {
        return Redirect(url);
    }
}

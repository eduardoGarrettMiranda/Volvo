using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VOLVO.Models;

namespace VOLVO.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

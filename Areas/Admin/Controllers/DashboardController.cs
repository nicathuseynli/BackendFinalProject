using Microsoft.AspNetCore.Mvc;

namespace Backend_Final_Project.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}


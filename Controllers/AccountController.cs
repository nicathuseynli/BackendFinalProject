using Microsoft.AspNetCore.Mvc;

namespace Backend_Final_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

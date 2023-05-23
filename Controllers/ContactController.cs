using Microsoft.AspNetCore.Mvc;

namespace Backend_Final_Project.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Backend_Final_Project.Controllers
{
    public class ViewCartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Backend_Final_Project.Data;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var newproduct = await _context.NewProducts.ToListAsync();
            ProductVM productVM = new ProductVM()
            {
                NewProducts = newproduct,
            };
            return View(productVM);
        }
    }
}

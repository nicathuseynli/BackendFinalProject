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

        public async Task<IActionResult> Index(int? id)
        {
            var homeproduct= await _context.HomeProducts.Include(c=>c.HomeCategory).FirstOrDefaultAsync(x=>x.Id==id);
            var newproducts = await _context.HomeProducts.ToListAsync();
            var realetedProduct = await _context.RealetedProducts.FirstOrDefaultAsync();
            var singleProduct = await _context.SingleProducts.FirstOrDefaultAsync();
            var singleProducts = await _context.SingleProducts.ToListAsync();
            ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

            AllProductVM productVM = new AllProductVM()
            {
                RealetedProducts = realetedProduct,
                Homeproduct = homeproduct,
                HomeProducts = newproducts,
                SingleProduct = singleProduct,
                SingleProducts = singleProducts
            };
            return View(productVM);
        }
    }
}

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
            var realetedProduct = await _context.RealetedProducts.FirstOrDefaultAsync();
            var singleProductPageInformations = await _context.SingleProductPageInformation.FirstOrDefaultAsync();
            var singleProductDescription = await _context.SingleProductDescription.FirstOrDefaultAsync();
            var singleProductReview = await _context.SingleProductReviews.ToListAsync();
            ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

            AllProductVM productVM = new AllProductVM()
            {
                RealetedProducts = realetedProduct,
                NewProducts = newproduct,
                SingleProductReviews = singleProductReview,
                SingleProductPageInformation = singleProductPageInformations,
                SingleProductDescription = singleProductDescription,
            };
            return View(productVM);
        }
    }
}

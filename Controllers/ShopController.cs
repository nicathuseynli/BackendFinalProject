using Backend_Final_Project.Data;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? categoryId,int? shoppagecolourId)
        {
            var query = _context.HomeProducts.Include(x => x.HomeCategory).AsQueryable();

            if (categoryId.HasValue && categoryId.Value>0 || shoppagecolourId.HasValue && shoppagecolourId.Value > 0)
            {
                query = query.Where(x => x.HomeCategoryId == categoryId||x.ShopPageColourId == shoppagecolourId);
            }
            var product = await query.ToListAsync(); 

            var category = await _context.HomeCategories.Include(x=>x.HomeProducts) .ToListAsync();
            var Color = await _context.ShopPageColours.Include(x=>x.HomeProducts) .ToListAsync();
            ShopVM shopVM = new()
            {
                HomeProducts = product,
                HomeCategories = category,
                ShopPageColours = Color
            };
            return View(shopVM);
        }
    }
}

using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Backend_Final_Project.Utilities.Pagination;
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

        public async Task<IActionResult> Index(int? categoryId,int? shoppagecolourId , int currentPage = 1 , int totalPageTake = 5)
        {
            var paginateprod= await _context.HomeProducts
                .Skip((currentPage - 1) * totalPageTake)
                .Take(totalPageTake)
                .ToListAsync();

            int dataCount = await _context.HomeProducts.CountAsync();

            int pageCount = await GetPageCount(totalPageTake);

            Paginate<HomeProduct> pagination = new(paginateprod, currentPage, pageCount);

            

            var query = _context.HomeProducts.Include(x => x.HomeCategory).AsQueryable();

            if (categoryId.HasValue && categoryId.Value>0 || shoppagecolourId.HasValue && shoppagecolourId.Value > 0)
            {
                query = query.Where(x => x.HomeCategoryId == categoryId||x.ShopPageColourId == shoppagecolourId);
            }
            var product = await query.ToListAsync(); 

            var category = await _context.HomeCategories.Include(x=>x.HomeProducts) .ToListAsync();
            var banner = await _context.Banners.FirstOrDefaultAsync();
            var Color = await _context.ShopPageColours.Include(x=>x.HomeProducts) .ToListAsync();
            ShopVM shopVM = new()
            {
                HomeProducts = product,
                HomeCategories = category,
                ShopPageColours = Color,
                Banner = banner,
                Paginates = pagination,
            };
            return View(shopVM);
        }
            private async Task<int> GetPageCount(int take)
            {
                int dataCount = await _context.HomeProducts.CountAsync();
                return (int)Math.Ceiling((decimal)dataCount / take);
            }
    }
}

using Backend_Final_Project.Data;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Controllers
{
    public class QuickLinkController : Controller
    {
        private readonly AppDbContext _context;

        public QuickLinkController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var quickLink = await _context.QuickLinks.FirstOrDefaultAsync();
            var socialMediaAddress = await _context.SocialMediaAdresses.FirstOrDefaultAsync();

            QuickLinkVM footerVM = new()
            {
                QuickLink = quickLink,
                SocialMediaAdress = socialMediaAddress,
            };

            return View(footerVM);
        }
    }
}

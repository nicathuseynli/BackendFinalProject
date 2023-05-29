using Backend_Final_Project.Data;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var quickLink = await _context.QuickLinks.FirstOrDefaultAsync();
            var socialMedia = await _context.SocialMediaAdresses.FirstOrDefaultAsync();
            FooterVM footerVM = new()
            {
                QuickLink = quickLink,
                SocialMediaAdress = socialMedia
            };
            return await Task.FromResult(View(footerVM));
        }
    }
}

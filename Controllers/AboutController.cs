using Backend_Final_Project.Data;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var aboutpages = await _context.AboutPages.ToListAsync();
            var companysliders = await _context.AboutCompanySliders.ToListAsync();
            var teammembers = await _context.AboutTeamMembers.ToListAsync();
            var membersingle = await _context.AboutTeamMembers.FirstOrDefaultAsync();
            AboutPageVM aboutpagevm = new AboutPageVM()
            {
                AboutPages = aboutpages,  
                AboutCompanySliders = companysliders,
                AboutTeamMembers = teammembers,  
                aboutTeam = membersingle,
            };
            return View(aboutpagevm);
        }
    }
}

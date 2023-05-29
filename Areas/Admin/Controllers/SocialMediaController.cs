using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class SocialMediaController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SocialMediaController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var socialmedia = await _context.SocialMediaAdresses.ToListAsync();
        return View(socialmedia);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSocialMediaVM createSocialMediaVM)
    {
        SocialMediaAdress socialmedia = new()
        {
            FacebookLink = createSocialMediaVM.FacebookLink,
            TwitterLink = createSocialMediaVM.TwitterLink,
            PinterestLink = createSocialMediaVM.PinterestLink,
            DribbbleLink = createSocialMediaVM.DribbbleLink,
        };
        await _context.SocialMediaAdresses.AddAsync(socialmedia);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var socialmedia = await _context.SocialMediaAdresses.FirstOrDefaultAsync(x => x.Id == id);
        if (socialmedia == null)
            return NotFound();
        return View(socialmedia);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var socialmedia = await _context.SocialMediaAdresses.FirstOrDefaultAsync(x => x.Id == id);
        if (socialmedia == null)
            return View();

        _context.SocialMediaAdresses.Remove(socialmedia);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var socialmedia = await _context.SocialMediaAdresses.FirstOrDefaultAsync(x => x.Id == id);
        if (socialmedia == null) return NotFound();

        var updateSocialMediaVM = new UpdateSocialMediaVM()
        {
            Id = socialmedia.Id,
            FacebookLink = socialmedia.FacebookLink,
            TwitterLink = socialmedia.TwitterLink,
            PinterestLink = socialmedia.PinterestLink,
            DribbbleLink = socialmedia.DribbbleLink,

        };
        return View(updateSocialMediaVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSocialMediaVM updateSocialMediaVM)
    {

        var socialmedia = await _context.SocialMediaAdresses.FirstOrDefaultAsync(x => x.Id == updateSocialMediaVM.Id);
        if (socialmedia == null) return NotFound();

        socialmedia.FacebookLink = updateSocialMediaVM.FacebookLink;
        socialmedia.TwitterLink = updateSocialMediaVM.TwitterLink;
        socialmedia.PinterestLink = updateSocialMediaVM.PinterestLink;
        socialmedia.DribbbleLink = updateSocialMediaVM.DribbbleLink;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

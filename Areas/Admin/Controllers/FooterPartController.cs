using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class FooterPartController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FooterPartController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
       _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        var quickLink = await _context.QuickLinks.ToListAsync();
        return View(quickLink);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateQuickLinkVM createQuickLinkVM)
    {

        QuickLink quickLink = new()
        {
            Description = createQuickLinkVM.Description,
            TelephoneNumber = createQuickLinkVM.TelephoneNumber,
        };
        await _context.QuickLinks.AddAsync(quickLink);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var quickLink = await _context.QuickLinks.FirstOrDefaultAsync(x => x.Id == id);
        if (quickLink == null)
            return NotFound();
        return View(quickLink);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var quickLink = await _context.QuickLinks.FirstOrDefaultAsync(x => x.Id == id);
        if (quickLink == null)
            return View();

        _context.QuickLinks.Remove(quickLink);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var quickLink = await _context.QuickLinks.FirstOrDefaultAsync(x => x.Id == id);
        if (quickLink == null) return NotFound();

        var updatequickLinkVM = new UpdateQuickLinkVM()
        {
            Id = quickLink.Id,
            Description = quickLink.Description,
            TelephoneNumber = quickLink.TelephoneNumber,

        };
        return View(updatequickLinkVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateQuickLinkVM updateQuickLinkVM)
    {

        var quickLink = await _context.QuickLinks.FirstOrDefaultAsync(x => x.Id == updateQuickLinkVM.Id);
        if (quickLink == null) return NotFound();

        quickLink.Description = updateQuickLinkVM.Description;
        quickLink.TelephoneNumber = updateQuickLinkVM.TelephoneNumber;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

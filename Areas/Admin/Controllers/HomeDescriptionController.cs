using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeDescriptionController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment webHostEnvironment;

    public HomeDescriptionController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var description = await _context.HomeDescriptions.ToListAsync();
        return View(description);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHomeDescriptionVM createHomeDescriptionVM)
    {
        if (!ModelState.IsValid)
            return View();
        HomeDescription description = new()
        {
            NewProductInfo = createHomeDescriptionVM.NewProductInfo,
            TestimonialInfo = createHomeDescriptionVM.TestimonialInfo,
            BlogInfo = createHomeDescriptionVM.BlogInfo,
        };
        await _context.HomeDescriptions.AddAsync(description);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var description = await _context.HomeDescriptions.FirstOrDefaultAsync(x => x.Id == id);
        if (description == null)
            return NotFound();
        return View(description);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var description = await _context.HomeDescriptions.FirstOrDefaultAsync(x => x.Id == id);
        if (description == null)
            return View();

        _context.HomeDescriptions.Remove(description);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var description = await _context.HomeDescriptions.FirstOrDefaultAsync(x => x.Id == id);
        if (description == null) return NotFound();

        var updateBlog = new UpdateHomeDescriptionVM()
        {
            Id = description.Id,
            NewProductInfo = description.NewProductInfo,
            TestimonialInfo = description.TestimonialInfo,
            BlogInfo = description.BlogInfo,
        };
        return View(updateBlog);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHomeDescriptionVM updateHomeDescriptionVM)
    {

        var description = await _context.HomeDescriptions.FirstOrDefaultAsync(x => x.Id == updateHomeDescriptionVM.Id);
        if (description == null) 
            return NotFound();

        description.NewProductInfo = updateHomeDescriptionVM.NewProductInfo;
        description.TestimonialInfo = updateHomeDescriptionVM.TestimonialInfo;
        description.BlogInfo = updateHomeDescriptionVM.BlogInfo;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

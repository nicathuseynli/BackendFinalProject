using Backend_Final_Project.Areas.Admin.ViewModels.HomeCategory;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class HomeCategoryController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HomeCategoryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult>Index()
    {
        var category = await _context.HomeCategories.ToListAsync();
        return View(category);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHomeCategoryVM createHomeCategoryVM)
    {

        HomeCategory category = new()
        {
            Name = createHomeCategoryVM.Name,
        };
        await _context.HomeCategories.AddAsync(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var category = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return NotFound();
        return View(category);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var category = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            return View();

        _context.HomeCategories.Remove(category);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var category = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == id);
        if (category == null) return NotFound();

        var updateHomeCategoryVM = new UpdateHomeCategoryVM()
        {
            Id = category.Id,
            Name = category.Name,
        };
        return View(updateHomeCategoryVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHomeCategoryVM updateHomeCategoryVM)
    {

        var category = await _context.HomeCategories.FirstOrDefaultAsync(x => x.Id == updateHomeCategoryVM.Id);
        if (category == null) return NotFound();

        category.Name = updateHomeCategoryVM.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}


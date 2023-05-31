using Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class SingleProductDescriptionController : Controller
{
    private readonly AppDbContext _context;

    public SingleProductDescriptionController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var singleProductDescription = await _context.SingleProductDescription.ToListAsync();
        return View(singleProductDescription);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSingleProductDescriptionVM createSingleProductDescription)
    {

        SingleProductDescription singleProductDescription = new()
        {
           Information =createSingleProductDescription.Information,
        };
        await _context.SingleProductDescription.AddAsync(singleProductDescription);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var singleProductDescription = await _context.SingleProductDescription.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProductDescription == null)
            return NotFound();
        return View(singleProductDescription);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var singleProductDescription = await _context.SingleProductDescription.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProductDescription == null)
            return View();

        _context.SingleProductDescription.Remove(singleProductDescription);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var singleProductDescription = await _context.SingleProductDescription.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProductDescription == null) return NotFound();

        var updateSingleProductDescriptionVM = new UpdateSingleProductDescriptionVM()
        {
            Id = singleProductDescription.Id,
       
        };
        return View(updateSingleProductDescriptionVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSingleProductDescriptionVM updateSingleProductDescriptionVM)
    {

        var singleProductDescription = await _context.SingleProductDescription.FirstOrDefaultAsync(x => x.Id == updateSingleProductDescriptionVM.Id);
        if (singleProductDescription == null) return NotFound();

        singleProductDescription.Information = updateSingleProductDescriptionVM.Information;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

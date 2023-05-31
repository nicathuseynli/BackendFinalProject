using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Backend_Final_Project.Areas.Admin.Controllers;

[Area("Admin")]
public class SingleProductRealetedController : Controller
{
    private readonly AppDbContext _context;

    public SingleProductRealetedController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var realetedProductinfo = await _context.RealetedProducts.ToListAsync();
        return View(realetedProductinfo);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateRealetedProductVM createRealetedProductVM)
    {

        RealetedProduct realetedProductinfo = new()
        {
            Information = createRealetedProductVM.Information,
        };
        await _context.RealetedProducts.AddAsync(realetedProductinfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var realetedProductinfo = await _context.RealetedProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (realetedProductinfo == null)
            return NotFound();
        return View(realetedProductinfo);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var realetedProductinfo = await _context.RealetedProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (realetedProductinfo == null)
            return View();

        _context.RealetedProducts.Remove(realetedProductinfo);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var realetedProductinfo = await _context.RealetedProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (realetedProductinfo == null) return NotFound();

        var updateRealetedProduct = new UpdateRealetedProductVM()
        {
            Id = realetedProductinfo.Id,
            Information = realetedProductinfo.Information,
        };
        return View(updateRealetedProduct);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateRealetedProductVM updateRealetedProductVM)
    {

        var realetedProductinfo = await _context.RealetedProducts.FirstOrDefaultAsync(x => x.Id == updateRealetedProductVM.Id);
        if (realetedProductinfo == null) return NotFound();

        realetedProductinfo.Information = updateRealetedProductVM.Information;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

using Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class ShopPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public ShopPageController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var shopPageColour = await _context.ShopPageColours.ToListAsync();
        return View(shopPageColour);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateShopPageColourVM createShopPageColourVM)
    {

        ShopPageColour shopPageColour = new()
        {
            Colour = createShopPageColourVM.Colour,
        };
        await _context.ShopPageColours.AddAsync(shopPageColour);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var shopPageColour = await _context.ShopPageColours.FirstOrDefaultAsync(x => x.Id == id);
        if (shopPageColour == null)
            return NotFound();
        return View(shopPageColour);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var shopPageColour = await _context.ShopPageColours.FirstOrDefaultAsync(x => x.Id == id);
        if (shopPageColour == null)
            return View();

        _context.ShopPageColours.Remove(shopPageColour);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var shopPageColour = await _context.ShopPageColours.FirstOrDefaultAsync(x => x.Id == id);
        if (shopPageColour == null) return NotFound();

        var updateShopPageColourVM = new UpdateShopPageColourVM()
        {
            Id = shopPageColour.Id,
            Colour = shopPageColour.Colour,
        };
        return View(updateShopPageColourVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateShopPageColourVM updateShopPageColourVM)
    {

        var shopPageColour = await _context.ShopPageColours.FirstOrDefaultAsync(x => x.Id == updateShopPageColourVM.Id);
        if (shopPageColour == null) return NotFound();

        shopPageColour.Colour = updateShopPageColourVM.Colour;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}


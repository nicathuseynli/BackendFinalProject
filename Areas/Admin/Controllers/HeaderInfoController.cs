using Backend_Final_Project.Areas.Admin.ViewModels.HeaderPart;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class HeaderInfoController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HeaderInfoController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        var headerInfo = await _context.HeaderInfos.ToListAsync();
        return View(headerInfo);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHeaderInfoVM createHeaderInfoVM)
    {

        HeaderInfo headerInfo = new()
        {
            Information = createHeaderInfoVM.Information,
        };
        await _context.HeaderInfos.AddAsync(headerInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync(x => x.Id == id);
        if (headerInfo == null)
            return NotFound();
        return View(headerInfo);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync(x => x.Id == id);
        if (headerInfo == null)
            return View();

        _context.HeaderInfos.Remove(headerInfo);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync(x => x.Id == id);
        if (headerInfo == null) return NotFound();

        var updateHeaderInfoVM = new UpdateHeaderInfoVM()
        {
            Id = headerInfo.Id,
            Information = headerInfo.Information,

        };
        return View(updateHeaderInfoVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHeaderInfoVM updateHeaderInfoVM)
    {

        var headerInfo = await _context.HeaderInfos.FirstOrDefaultAsync(x => x.Id == updateHeaderInfoVM.Id);
        if (headerInfo == null) return NotFound();

        headerInfo.Information = updateHeaderInfoVM.Information;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

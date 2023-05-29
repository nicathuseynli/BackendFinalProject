using Backend_Final_Project.Areas.Admin.ViewModels.HeaderPart;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class HeaderPhoneController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public HeaderPhoneController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        var headerPhoneNumber = await _context.HeaderPhoneNumbers.ToListAsync();
        return View(headerPhoneNumber);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateHeaderPhoneNumberVM createHeaderPhoneNumberVM)
    {

        HeaderPhoneNumber headerPhoneNumber = new()
        {
            PhoneNumber = createHeaderPhoneNumberVM.PhoneNumber,
        };
        await _context.HeaderPhoneNumbers.AddAsync(headerPhoneNumber);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var headerPhoneNumber = await _context.HeaderPhoneNumbers.FirstOrDefaultAsync(x => x.Id == id);
        if (headerPhoneNumber == null)
            return NotFound();
        return View(headerPhoneNumber);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var headerPhoneNumber = await _context.HeaderPhoneNumbers.FirstOrDefaultAsync(x => x.Id == id);
        if (headerPhoneNumber == null)
            return View();

        _context.HeaderPhoneNumbers.Remove(headerPhoneNumber);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var headerPhoneNumber = await _context.HeaderPhoneNumbers.FirstOrDefaultAsync(x => x.Id == id);
        if (headerPhoneNumber == null) return NotFound();

        var updateHeaderPhoneNumberVM = new UpdateHeaderPhoneNumberVM()
        {
            Id = headerPhoneNumber.Id,
            PhoneNumber = headerPhoneNumber.PhoneNumber,

        };
        return View(updateHeaderPhoneNumberVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateHeaderPhoneNumberVM updateHeaderPhoneNumberVM)
    {

        var headerPhoneNumber = await _context.HeaderPhoneNumbers.FirstOrDefaultAsync(x => x.Id == updateHeaderPhoneNumberVM.Id);
        if (headerPhoneNumber == null) return NotFound();

        headerPhoneNumber.PhoneNumber = updateHeaderPhoneNumberVM.PhoneNumber;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

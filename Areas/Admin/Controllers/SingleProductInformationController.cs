using Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;

[Area("Admin")]
public class SingleProductInformation : Controller
{
    private readonly AppDbContext _context;

    public SingleProductInformation(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var singleProductInformation = await _context.SingleProductPageInformation.ToListAsync();
        return View(singleProductInformation);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSingleProductInformationVM createSingleProductInformationVM)
    {

        SingleProductPageInformation singleProductPageInformation = new()
        {
            AboutReturnInfo = createSingleProductInformationVM.AboutReturnInfo,
            GuaranteeInfo = createSingleProductInformationVM.GuaranteeInfo,
            ShippingInfo = createSingleProductInformationVM.ShippingInfo,
        };
        await _context.SingleProductPageInformation.AddAsync(singleProductPageInformation);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var singleProductPageInformation = await _context.SingleProductPageInformation.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProductPageInformation == null)
            return NotFound();
        return View(singleProductPageInformation);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var singleProductPageInformation = await _context.SingleProductPageInformation.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProductPageInformation == null)
            return View();

        _context.SingleProductPageInformation.Remove(singleProductPageInformation);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var singleProductPageInformation = await _context.SingleProductPageInformation.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProductPageInformation == null) return NotFound();

        var updateSingleProductInformationVM = new UpdateSingleProductInformationVM()
        {
            Id = singleProductPageInformation.Id,
            AboutReturnInfo = singleProductPageInformation.AboutReturnInfo,
            GuaranteeInfo = singleProductPageInformation.GuaranteeInfo,
            ShippingInfo = singleProductPageInformation.ShippingInfo,
        };
        return View(updateSingleProductInformationVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSingleProductInformationVM updateSingleProductInformationVM)
    {

        var singleProductPageInformation = await _context.SingleProductPageInformation.FirstOrDefaultAsync(x => x.Id == updateSingleProductInformationVM.Id);
        if (singleProductPageInformation == null) return NotFound();

        singleProductPageInformation.AboutReturnInfo = updateSingleProductInformationVM.AboutReturnInfo;
        singleProductPageInformation.GuaranteeInfo = updateSingleProductInformationVM.GuaranteeInfo;
        singleProductPageInformation.ShippingInfo = updateSingleProductInformationVM.ShippingInfo;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
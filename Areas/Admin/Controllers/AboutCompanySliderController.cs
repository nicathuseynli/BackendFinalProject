using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;

[Area("Admin")]
public class AboutCompanySliderController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AboutCompanySliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var aboutcompanysliderpages = await _context.AboutCompanySliders.ToListAsync();
        return View(aboutcompanysliderpages);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAboutCompanySliderVM createAboutCompanySliderVM)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createAboutCompanySliderVM.Photo.ContentType.Contains("image/"))
            return View();

        if (createAboutCompanySliderVM.Photo.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createAboutCompanySliderVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createAboutCompanySliderVM.Photo.CopyToAsync(stream);

        AboutCompanySlider companySlider = new()
        {
            Image = filename
        };
        await _context.AboutCompanySliders.AddAsync(companySlider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var companySlider = await _context.AboutCompanySliders.FirstOrDefaultAsync(x => x.Id == id);
        if (companySlider == null)
            return NotFound();
        return View(companySlider);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var companySlider = await _context.AboutCompanySliders.FirstOrDefaultAsync(x => x.Id == id);
        if (companySlider == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", companySlider.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.AboutCompanySliders.Remove(companySlider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var companySlider = await _context.AboutCompanySliders.FirstOrDefaultAsync(x => x.Id == id);
        if (companySlider == null) return NotFound();

        var updateCompanySlider = new UpdateAboutCompanySliderVM()
        {
            Id = companySlider.Id,
        };
        return View(updateCompanySlider);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAboutCompanySliderVM updateAboutCompanySliderVM)
    {

        var companySlider = await _context.AboutCompanySliders.FirstOrDefaultAsync(x => x.Id == updateAboutCompanySliderVM.Id);
        if (companySlider == null) return NotFound();

        if (updateAboutCompanySliderVM.Photo != null)
        {
            if (updateAboutCompanySliderVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateAboutCompanySliderVM.Photo.Length / 1024 > 500)
                return View();


            string filename = updateAboutCompanySliderVM.Photo.FileName + " _ " + Guid.NewGuid().ToString();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateAboutCompanySliderVM.Photo.CopyToAsync(stream);

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", companySlider.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            companySlider.Image = filename;
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

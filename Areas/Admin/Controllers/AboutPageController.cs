using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class AboutPageController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AboutPageController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var aboutuspages = await _context.AboutPages.ToListAsync();
        return View(aboutuspages);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAboutUsVM createAboutUsVM)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createAboutUsVM.Photo.ContentType.Contains("image/"))
            return View();

        if (createAboutUsVM.Photo.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createAboutUsVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createAboutUsVM.Photo.CopyToAsync(stream);

        About aboutuspage = new()
        {
            Information = createAboutUsVM.Information,
            VideoLink = createAboutUsVM.VideoLink,
            CenterTitle = createAboutUsVM.CenterTitle,
            Image = filename
        };
        await _context.AboutPages.AddAsync(aboutuspage);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var aboutuspage = await _context.AboutPages.FirstOrDefaultAsync(x => x.Id == id);
        if (aboutuspage == null)
            return NotFound();
        return View(aboutuspage);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var aboutuspage = await _context.AboutPages.FirstOrDefaultAsync(x => x.Id == id);
        if (aboutuspage == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", aboutuspage.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.AboutPages.Remove(aboutuspage);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var aboutuspage = await _context.AboutPages.FirstOrDefaultAsync(x => x.Id == id);
        if (aboutuspage == null) return NotFound();

        var updateAboutusPage = new UpdateAboutUsVM()
        {
            Id = aboutuspage.Id,
            Information = aboutuspage.Information,
            VideoLink = aboutuspage.VideoLink,
            CenterTitle = aboutuspage.CenterTitle,
            Image = aboutuspage.Image,
        };
        return View(updateAboutusPage);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAboutUsVM updateAboutUsVM)
    {

        var aboutus = await _context.AboutPages.FirstOrDefaultAsync(x => x.Id == updateAboutUsVM.Id);
        if (aboutus == null) return NotFound();

        if (updateAboutUsVM.Photo != null)
        {
            #region Create NewImage
            if (!updateAboutUsVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateAboutUsVM.Photo.Length / 1024 > 1000)
                return View();


            string filename = Guid.NewGuid().ToString() + " _ " + updateAboutUsVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateAboutUsVM.Photo.CopyToAsync(stream);
            #endregion
            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", aboutus.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            aboutus.Image = filename;
            #endregion
        }

        aboutus.Information = updateAboutUsVM.Information;
        aboutus.VideoLink = updateAboutUsVM.VideoLink;
        aboutus.CenterTitle = updateAboutUsVM.CenterTitle;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

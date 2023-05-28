using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class BannerController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BannerController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        var banner = await _context.Banners.ToListAsync();
        return View(banner);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBannerVM createbannerVM)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createbannerVM.Photo.ContentType.Contains("image/"))
            return View();

        if (createbannerVM.Photo.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createbannerVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createbannerVM.Photo.CopyToAsync(stream);

        Banner banner = new()
        {
            Description = createbannerVM.Description,
            Title = createbannerVM.Title,
            BannerImage = filename
        };
        await _context.Banners.AddAsync(banner);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null)
            return NotFound();
        return View(banner);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", banner.BannerImage);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == id);
        if (banner == null) return NotFound();

        var updateBanner = new UpdateBannerVM()
        {
            Id = banner.Id,
            Description = banner.Description,
            Title = banner.Title,
            BannerImage = banner.BannerImage,
        };
        return View(updateBanner);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBannerVM updateBannerVM)
    {

        var banner = await _context.Banners.FirstOrDefaultAsync(x => x.Id == updateBannerVM.Id);
        if (banner == null) return NotFound();

        if (updateBannerVM.Photo != null)
        {
            #region Create NewImage
            if (!updateBannerVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateBannerVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateBannerVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateBannerVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", banner.BannerImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            banner.BannerImage = filename;
            #endregion
        }
        banner.Description = updateBannerVM.Description;
        banner.Title = updateBannerVM.Title;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}


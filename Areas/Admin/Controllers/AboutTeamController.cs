using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class AboutTeamController : Controller
{
    private readonly AppDbContext _context;
    private IWebHostEnvironment _webHostEnvironment;

    public AboutTeamController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var abouteam = await _context.AboutTeamMembers.ToListAsync();
        return View(abouteam);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAboutTeamVM createAboutTeamVM)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createAboutTeamVM.Photo.ContentType.Contains("image/"))
            return View();

        if (createAboutTeamVM.Photo.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createAboutTeamVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createAboutTeamVM.Photo.CopyToAsync(stream);

        AboutTeam abouteam = new()
        {
            Id = createAboutTeamVM.Id,
            Description = createAboutTeamVM.Description,
            Name = createAboutTeamVM.Name,
            Surname = createAboutTeamVM.Surname,
            Profession = createAboutTeamVM.Profession,
            Image = filename
        };
        await _context.AboutTeamMembers.AddAsync(abouteam);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var abouteam = await _context.AboutTeamMembers.FirstOrDefaultAsync(x => x.Id == id);
        if (abouteam == null)
            return NotFound();
        return View(abouteam);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var abouteam = await _context.AboutTeamMembers.FirstOrDefaultAsync(x => x.Id == id);
        if (abouteam == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", abouteam.Image);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.AboutTeamMembers.Remove(abouteam);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var abouteam = await _context.AboutTeamMembers.FirstOrDefaultAsync(x => x.Id == id);
        if (abouteam == null) return NotFound();

        var updateTeamMember = new UpdateAboutTeamVM()
        {
            Id = abouteam.Id,
            Description = abouteam.Description,
            Name = abouteam.Name,
            Surname = abouteam.Surname,
            Profession = abouteam.Profession,
            Image=abouteam.Image,
        };
        return View(updateTeamMember);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAboutTeamVM updateAboutTeamVM)
    {

        var abouteam = await _context.AboutTeamMembers.FirstOrDefaultAsync(x => x.Id == updateAboutTeamVM.Id);
        if (abouteam == null) return NotFound();

        if (updateAboutTeamVM.Photo != null)
        {
            #region Create NewImage
            if (!updateAboutTeamVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateAboutTeamVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateAboutTeamVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateAboutTeamVM.Photo.CopyToAsync(stream);
            #endregion
            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", abouteam.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            abouteam.Image = filename;
            #endregion

        }
        abouteam.Description = updateAboutTeamVM.Description;
        abouteam.Name = updateAboutTeamVM.Name;
        abouteam.Surname = updateAboutTeamVM.Surname;
        abouteam.Profession = updateAboutTeamVM.Profession;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

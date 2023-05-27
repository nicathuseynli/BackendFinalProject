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
        };
        return View(updateTeamMember);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateAboutTeamVM UpdateAboutTeamVM)
    {

        var abouteam = await _context.AboutTeamMembers.FirstOrDefaultAsync(x => x.Id == UpdateAboutTeamVM.Id);
        if (abouteam == null) return NotFound();

        if (UpdateAboutTeamVM.Photo != null)
        {
            if (UpdateAboutTeamVM.Photo.ContentType.Contains("image/"))
                return View();

            if (UpdateAboutTeamVM.Photo.Length / 1024 > 500)
                return View();


            string filename = UpdateAboutTeamVM.Photo.FileName + " _ " + Guid.NewGuid().ToString();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await UpdateAboutTeamVM.Photo.CopyToAsync(stream);

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", abouteam.Image);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            abouteam.Image = filename;
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

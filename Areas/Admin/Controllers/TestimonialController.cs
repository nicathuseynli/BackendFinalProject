using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class TestimonialController : Controller
{
    private AppDbContext _context;
    public IWebHostEnvironment _webHostEnvironment;
    public TestimonialController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var testimonial = await _context.Testimonials.ToListAsync();
        return View(testimonial);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateTestimonialVM createTestimonialVm)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createTestimonialVm.Photo.ContentType.Contains("image/"))
            return View();

        if (createTestimonialVm.Photo.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createTestimonialVm.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createTestimonialVm.Photo.CopyToAsync(stream);

        Testimonial testimonial = new()
        {
            Comment = createTestimonialVm.Comment,
            FullName = createTestimonialVm.FullName,
            Profession = createTestimonialVm.Profession,
            ClientImage = filename
        };
        await _context.Testimonials.AddAsync(testimonial);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null)
            return NotFound();
        return View(testimonial);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", testimonial.ClientImage);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Testimonials.Remove(testimonial);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == id);
        if (testimonial == null) return NotFound();

        var updateTestimonialVM = new UpdateTestimonialVM()
        {
            Id = id,
            Profession = testimonial.Profession,
            FullName = testimonial.FullName,
            Comment = testimonial.Comment,
            ClientImage = testimonial.ClientImage,
        };
        return View(updateTestimonialVM);
    }
    [HttpPost]
    public async Task<IActionResult> Update(UpdateTestimonialVM updateTestimonialVM)
    {

        var testimonial = await _context.Testimonials.FirstOrDefaultAsync(x => x.Id == updateTestimonialVM.Id);
        if (testimonial == null) return NotFound();

        if (updateTestimonialVM.Photo != null)
        {
            if (updateTestimonialVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateTestimonialVM.Photo.Length / 1024 > 500)
                return View();


            string filename = updateTestimonialVM.Photo.FileName + " _ " + Guid.NewGuid().ToString();
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateTestimonialVM.Photo.CopyToAsync(stream);

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", testimonial.ClientImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            testimonial.ClientImage = filename;
        }

        testimonial.Comment = updateTestimonialVM.Comment;
        testimonial.Profession = updateTestimonialVM.Profession;
        testimonial.FullName = updateTestimonialVM.FullName;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

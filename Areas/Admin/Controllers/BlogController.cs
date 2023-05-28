using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class BlogController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BlogController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }


    public async Task<IActionResult> Index()
    {
        var blog = await _context.Blogs.ToListAsync();
        return View(blog);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateBlogVM createblogVM)
    { 
        if (!ModelState.IsValid)
            return View();

        if (!createblogVM.Photo.ContentType.Contains("image/"))
            return View();

        if (createblogVM.Photo.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createblogVM.Photo.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createblogVM.Photo.CopyToAsync(stream);

        Blog blog = new()
        {
            ByPerson = createblogVM.ByPerson,
            BlogInformation = createblogVM.BlogInformation,
            BlogTitle = createblogVM.BlogTitle,
            BlogImage = filename
        };
        await _context.Blogs.AddAsync(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null)
            return NotFound();
        return View(blog);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.BlogImage);

        if (System.IO.File.Exists(path))
            System.IO.File.Delete(path);

        System.IO.File.Delete(path);

        _context.Blogs.Remove(blog);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == id);
        if (blog == null) return NotFound();

        var updateBlog = new UpdateBlogVM()
        {
            Id = blog.Id,
            BlogInformation = blog.BlogInformation,
            BlogTitle = blog.BlogTitle,
            ByPerson = blog.ByPerson,
            BlogImage = blog.BlogImage,
        };
        return View(updateBlog);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateBlogVM updateBlogVM)
    {

        var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == updateBlogVM.Id);
        if (blog == null) return NotFound();

        if (updateBlogVM.Photo != null)
        {
            #region Create NewImage
            if (!updateBlogVM.Photo.ContentType.Contains("image/"))
                return View();

            if (updateBlogVM.Photo.Length / 1024 > 1000)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateBlogVM.Photo.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateBlogVM.Photo.CopyToAsync(stream);
            #endregion

            #region DeleteOldImage
            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", blog.BlogImage);
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            blog.BlogImage = filename;
            #endregion
        }
        blog.BlogInformation = updateBlogVM.BlogInformation;
        blog.BlogTitle = updateBlogVM.BlogTitle;
        blog.ByPerson = updateBlogVM.ByPerson;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

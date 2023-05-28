using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class NewProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public NewProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _environment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var newproduct = await _context.NewProducts.ToListAsync();
        return View(newproduct);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateNewProductVM createNewProductVM)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createNewProductVM.Photo.ContentType.Contains("image/") && !createNewProductVM.HoverPhoto.ContentType.Contains("image/"))
            return View();

        if (createNewProductVM.Photo.Length / 1024 > 500 && createNewProductVM.HoverPhoto.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createNewProductVM.Photo.FileName;
        string hoverfilename = Guid.NewGuid().ToString() + "_" + createNewProductVM.HoverPhoto.FileName;

        string path = Path.Combine(_environment.WebRootPath, "images", filename);
        string hoverpath = Path.Combine(_environment.WebRootPath, "images", hoverfilename);

        using FileStream stream = new FileStream(path, FileMode.Create);
        using FileStream hoverstream = new FileStream(hoverpath, FileMode.Create);

        await createNewProductVM.Photo.CopyToAsync(stream);
        await createNewProductVM.HoverPhoto.CopyToAsync(hoverstream);


        NewProduct newproduct = new()
        {
            Name = createNewProductVM.Name,
            Rating = createNewProductVM.Rating,
            Price = createNewProductVM.Price,
            HoverImg = hoverfilename,
            ProductImg = filename
        };
        await _context.NewProducts.AddAsync(newproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var newproduct = await _context.NewProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (newproduct == null)
            return NotFound();
        return View(newproduct);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var newproduct = await _context.NewProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (newproduct == null)
            return View();

        string path = Path.Combine(_environment.WebRootPath, "images", newproduct.ProductImg);
        string hoverpath = Path.Combine(_environment.WebRootPath, "images", newproduct.HoverImg);

        if (System.IO.File.Exists(path) && System.IO.File.Exists(hoverpath))
        {
            System.IO.File.Delete(path);
            System.IO.File.Delete(hoverpath);
        }

        System.IO.File.Delete(path);

        _context.NewProducts.Remove(newproduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var newproduct = await _context.NewProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (newproduct == null) return NotFound();

        var updateNewProductVM = new UpdateNewProductVM()
        {
            Id = id,
            Name = newproduct.Name,
            Price = newproduct.Price,
            Rating = newproduct.Rating,
            ProductImg = newproduct.ProductImg,
            HoverImg = newproduct.HoverImg,
        };

        return View(updateNewProductVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateNewProductVM updateNewProductVM)
    {

        var newproduct = await _context.NewProducts.FirstOrDefaultAsync(x => x.Id == updateNewProductVM.Id);
        if (newproduct == null) return NotFound();

        if (updateNewProductVM.Photo != null && updateNewProductVM.HoverPhoto !=null)
        {
            if (!updateNewProductVM.Photo.ContentType.Contains("image/") && !updateNewProductVM.HoverPhoto.ContentType.Contains("image/"))
                return View();

            if (updateNewProductVM.Photo.Length / 1024 > 500 && updateNewProductVM.HoverPhoto.Length / 1024 > 500)
                return View();

            string filename = Guid.NewGuid().ToString() + " _ " + updateNewProductVM.Photo.FileName;
            string hoverfilename = Guid.NewGuid().ToString() + " _ " + updateNewProductVM.HoverPhoto.FileName;

            string path = Path.Combine(_environment.WebRootPath, "images", filename);
            string hoverpath = Path.Combine(_environment.WebRootPath, "images", hoverfilename);

            using FileStream stream = new FileStream(path, FileMode.Create);
            using FileStream hoverstream = new FileStream(hoverpath, FileMode.Create);

            await updateNewProductVM.Photo.CopyToAsync(stream);
            await updateNewProductVM.HoverPhoto.CopyToAsync(hoverstream);

            string oldPath = Path.Combine(_environment.WebRootPath, "images", newproduct.ProductImg);
            string oldHoverPath = Path.Combine(_environment.WebRootPath, "images", newproduct.HoverImg);
            if (System.IO.File.Exists(oldPath) && System.IO.File.Exists(oldHoverPath))
            {
                System.IO.File.Delete(oldPath);
                System.IO.File.Delete(oldHoverPath);
            }
            newproduct.ProductImg = filename;
            newproduct.HoverImg = hoverfilename;
        }

        newproduct.Name = updateNewProductVM.Name;
        newproduct.Price = updateNewProductVM.Price;
        newproduct.Rating = updateNewProductVM.Rating;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
 

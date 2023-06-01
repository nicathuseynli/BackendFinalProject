using Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class SingleProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public SingleProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task<IActionResult> Index()
    {
        var singleProduct = await _context.SingleProducts.ToListAsync();
        return View(singleProduct);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSingleProductVM createSingleProductVM)
    {
        if (!ModelState.IsValid)
            return View();

        if (!createSingleProductVM.UserPhoto.ContentType.Contains("image/"))
            return View();

        if (createSingleProductVM.UserPhoto.Length / 1024 > 500)
            return View();

        string filename = Guid.NewGuid().ToString() + "_" + createSingleProductVM.UserPhoto.FileName;

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

        using FileStream stream = new FileStream(path, FileMode.Create);

        await createSingleProductVM.UserPhoto.CopyToAsync(stream);

        SingleProduct singleProduct = new()
        {
            Information = createSingleProductVM.Information,
            UserFullname = createSingleProductVM.UserFullname,
            UserComment = createSingleProductVM.UserComment,
            CommentCount = createSingleProductVM.CommentCount,
            AboutReturnInfo = createSingleProductVM.AboutReturnInfo,
            GuaranteeInfo = createSingleProductVM.GuaranteeInfo,
            ShippingInfo = createSingleProductVM.ShippingInfo,
            UserImage = filename,
        };
        await _context.SingleProducts.AddAsync(singleProduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var singleProduct = await _context.SingleProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProduct == null)
            return NotFound();
        return View(singleProduct);
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {

        var singleProduct = await _context.SingleProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProduct == null)
            return View();

        string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", singleProduct.UserImage);

        if (System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }

        _context.SingleProducts.Remove(singleProduct);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));

    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var singleProduct = await _context.SingleProducts.FirstOrDefaultAsync(x => x.Id == id);
        if (singleProduct == null) return NotFound();

        var updateSingleProductVM = new UpdateSingleProductVM()
        {
            Id = id,
            Information = singleProduct.Information,
            UserFullname = singleProduct.UserFullname,
            UserComment = singleProduct.UserComment,
            CommentCount = singleProduct.CommentCount,
            AboutReturnInfo = singleProduct.AboutReturnInfo,
            GuaranteeInfo = singleProduct.GuaranteeInfo,
            ShippingInfo = singleProduct.ShippingInfo,
        };

        return View(updateSingleProductVM);

    }

    [HttpPost]
    public async Task<IActionResult> Update(UpdateSingleProductVM updateSingleProductVM)
    {

        var singleProduct = await _context.SingleProducts.FirstOrDefaultAsync(x => x.Id == updateSingleProductVM.Id);
        if (singleProduct == null) return NotFound();

        if (updateSingleProductVM.UserPhoto != null)
        {
            if (updateSingleProductVM.UserPhoto.ContentType.Contains("image/"))
                return View();

            if (updateSingleProductVM.UserPhoto.Length / 1024 > 500)
                return View();


            string filename = Guid.NewGuid().ToString() + " _ " + updateSingleProductVM.UserPhoto.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await updateSingleProductVM.UserPhoto.CopyToAsync(stream);

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", updateSingleProductVM.UserImage);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            updateSingleProductVM.UserImage = filename;
        }

        singleProduct.UserFullname = updateSingleProductVM.UserFullname;
        singleProduct.UserComment = updateSingleProductVM.UserComment;
        singleProduct.CommentCount = updateSingleProductVM.CommentCount;
        singleProduct.Information = updateSingleProductVM.Information;
        singleProduct.GuaranteeInfo = updateSingleProductVM.GuaranteeInfo;
        singleProduct.AboutReturnInfo = updateSingleProductVM.AboutReturnInfo;
        singleProduct.ShippingInfo = updateSingleProductVM.ShippingInfo;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

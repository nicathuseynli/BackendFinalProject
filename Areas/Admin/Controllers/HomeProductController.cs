using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HomeProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public HomeProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var homeproduct = await _context.HomeProducts.ToListAsync();
            return View(homeproduct);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(),"Id","Name" );
            ViewBag.Colour = new SelectList(await _context.ShopPageColours.ToListAsync(),"Id","Colour" );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeProductVM createhomeproductVm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Name");
                ViewBag.Colour = new SelectList(await _context.ShopPageColours.ToListAsync(), "Id", "Colour");
                return View();
            }

            if (!createhomeproductVm.Photo.ContentType.Contains("image/") && !createhomeproductVm.HoverPhoto.ContentType.Contains("image/"))
                return View();

            if (createhomeproductVm.Photo.Length / 1024 > 500 && createhomeproductVm.HoverPhoto.Length / 1024 > 500)
                return View();

            string filename = Guid.NewGuid().ToString() + "_" + createhomeproductVm.Photo.FileName;
            string Hoverfilename = Guid.NewGuid().ToString() + "_" + createhomeproductVm.HoverPhoto.FileName;

            string path = Path.Combine(_environment.WebRootPath, "images", filename);
            string Hoverpath = Path.Combine(_environment.WebRootPath, "images", Hoverfilename);

            using FileStream stream = new FileStream(path, FileMode.Create);
            using FileStream Hoverstream = new FileStream(Hoverpath, FileMode.Create);

            await createhomeproductVm.Photo.CopyToAsync(stream);
            await createhomeproductVm.HoverPhoto.CopyToAsync(Hoverstream);

            HomeProduct homeproduct = new()
            {
                Name = createhomeproductVm.Name,
                Rating = createhomeproductVm.Rating,
                Price = createhomeproductVm.Price,
                Description = createhomeproductVm.Description,
                HomeCategoryId = createhomeproductVm.HomeCategoryId,
                ShopPageColourId = createhomeproductVm.ShopPageColourId,
                Image = filename,
                HoverImage = Hoverfilename,
            };
            await _context.HomeProducts.AddAsync(homeproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (homeproduct == null)
                return NotFound();
            return View(homeproduct);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (homeproduct == null)
                return View();

            string path = Path.Combine(_environment.WebRootPath, "images", homeproduct.Image);
            string Hoverpath = Path.Combine(_environment.WebRootPath, "images", homeproduct.HoverImage);

            if (System.IO.File.Exists(path) && System.IO.File.Exists(Hoverpath))
            {
                System.IO.File.Delete(path);
                System.IO.File.Delete(Hoverpath);
            }

            _context.HomeProducts.Remove(homeproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Name");
            ViewBag.Colour = new SelectList(await _context.ShopPageColours.ToListAsync(), "Id", "Colour");

            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (homeproduct == null) return NotFound();

            var updateHomeProductVM = new UpdateHomeProductVM()
            {
                Id = id,
                HomeCategoryId = homeproduct.HomeCategoryId,
                ShopPageColourId = homeproduct.ShopPageColourId,
                Name = homeproduct.Name,
                Price = homeproduct.Price,
                Rating = homeproduct.Rating,
                Description = homeproduct.Description,
                Image = homeproduct.Image,
                HoverImage = homeproduct.HoverImage,
                
            };

            return View(updateHomeProductVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateHomeProductVM updateHomeProductVM)
        {

            ViewBag.Category = new SelectList(await _context.HomeCategories.ToListAsync(), "Id", "Name");
            ViewBag.Colour = new SelectList(await _context.ShopPageColours.ToListAsync(), "Id", "Colour");

            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == updateHomeProductVM.Id);
            if (homeproduct == null) return NotFound();

            if (updateHomeProductVM.Photo != null && updateHomeProductVM.HoverPhoto != null)
            {
                if (updateHomeProductVM.Photo.ContentType.Contains("image/") && updateHomeProductVM.HoverPhoto.ContentType.Contains("image/"))
                    return View();

                if (updateHomeProductVM.Photo.Length / 1024 > 500 && updateHomeProductVM.HoverPhoto.Length / 1024 > 500)
                    return View();


                string filename = Guid.NewGuid().ToString() + " _ " + updateHomeProductVM.Photo.FileName ;
                string Hoverfilename = Guid.NewGuid().ToString() + " _ " + updateHomeProductVM.HoverPhoto.FileName ;
                string path = Path.Combine(_environment.WebRootPath, "images", filename);
                string Hoverpath = Path.Combine(_environment.WebRootPath, "images", Hoverfilename);

                using FileStream stream = new FileStream(path, FileMode.Create);
                using FileStream Hoverstream = new FileStream(Hoverpath, FileMode.Create);

                await updateHomeProductVM.Photo.CopyToAsync(stream);
                await updateHomeProductVM.HoverPhoto.CopyToAsync(Hoverstream);

                string oldPath = Path.Combine(_environment.WebRootPath, "images", homeproduct.Image);
                string oldHoverPath = Path.Combine(_environment.WebRootPath, "images", homeproduct.HoverImage);
                if (System.IO.File.Exists(oldPath) && System.IO.File.Exists(oldHoverPath))
                {
                    System.IO.File.Delete(oldPath);
                    System.IO.File.Delete(oldHoverPath);
                }
                 
                homeproduct.Image = filename;
                homeproduct.HoverImage = Hoverfilename;
            }

            homeproduct.Name = updateHomeProductVM.Name;
            homeproduct.Price = updateHomeProductVM.Price;
            homeproduct.Rating = updateHomeProductVM.Rating;
            homeproduct.Description = updateHomeProductVM.Description;
            homeproduct.HomeCategoryId = updateHomeProductVM.HomeCategoryId;
            homeproduct.ShopPageColourId = updateHomeProductVM.ShopPageColourId;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

    //ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", mcat.CategoryId);
}


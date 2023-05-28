using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateHomeProductVM createhomeproductVm)
        {
            if (!ModelState.IsValid)
                return View();

            if (!createhomeproductVm.Photo.ContentType.Contains("image/"))
                return View();

            if (createhomeproductVm.Photo.Length / 1024 > 500)
                return View();

            string filename = Guid.NewGuid().ToString() + "_" + createhomeproductVm.Photo.FileName;

            string path = Path.Combine(_environment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await createhomeproductVm.Photo.CopyToAsync(stream);

            HomeProduct homeproduct = new()
            {
                Name = createhomeproductVm.Name,
                Rating = createhomeproductVm.Rating,
                Price = createhomeproductVm.Price,
                Image = filename
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

            var homeProductCount = await _context.HomeProducts.CountAsync();
            if (homeProductCount <= 2)
                return RedirectToAction(nameof(Index));


            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (homeproduct == null)
                return View();

            string path = Path.Combine(_environment.WebRootPath, "images", homeproduct.Image);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            System.IO.File.Delete(path);

            _context.HomeProducts.Remove(homeproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == id);
            if (homeproduct == null) return NotFound();

            var updateHomeProductVM = new UpdateHomeProductVM()
            {
                Id = id,
                Name = homeproduct.Name,
                Price = homeproduct.Price,
                Rating = homeproduct.Rating,
                Image = homeproduct.Image,
            };

            return View(updateHomeProductVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateHomeProductVM updateHomeProductVM)
        {

            var homeproduct = await _context.HomeProducts.FirstOrDefaultAsync(x => x.Id == updateHomeProductVM.Id);
            if (homeproduct == null) return NotFound();

            if (updateHomeProductVM.Photo != null)
            {
                if (updateHomeProductVM.Photo.ContentType.Contains("image/"))
                    return View();

                if (updateHomeProductVM.Photo.Length / 1024 > 500)
                    return View();


                string filename = Guid.NewGuid().ToString() + " _ " + updateHomeProductVM.Photo.FileName ;
                string path = Path.Combine(_environment.WebRootPath, "images", filename);

                using FileStream stream = new FileStream(path, FileMode.Create);

                await updateHomeProductVM.Photo.CopyToAsync(stream);

                string oldPath = Path.Combine(_environment.WebRootPath, "images", homeproduct.Image);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
                homeproduct.Image = filename;
            }

            homeproduct.Name = updateHomeProductVM.Name;
            homeproduct.Price = updateHomeProductVM.Price;
            homeproduct.Rating = updateHomeProductVM.Rating;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}


using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _environment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        { 
            var sliders = await _context.Sliders.ToListAsync();
            return View(sliders);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSliderVM createSliderVm)
        {
            if (!ModelState.IsValid)
                return View();

            if (!createSliderVm.Photo.ContentType.Contains("image/"))
                return View();

            if (createSliderVm.Photo.Length / 1024 > 500)
                return View();

            string filename = Guid.NewGuid().ToString() + "_" + createSliderVm.Photo.FileName;

            string path = Path.Combine(_environment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await createSliderVm.Photo.CopyToAsync(stream);

            Slider slider = new()
            {
                Label = createSliderVm.Label,
                Description = createSliderVm.Description,
                Percent = createSliderVm.Percent,
                Image = filename
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
                return NotFound();
            return View(slider);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var sliderCount = await _context.Sliders.CountAsync();
            if (sliderCount <= 2)
                return RedirectToAction(nameof(Index));


            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null)
                return View();

            string path = Path.Combine(_environment.WebRootPath, "img", slider.Image);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            System.IO.File.Delete(path);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider == null) return NotFound();

            var updateSliderVM = new UpdateSliderVM()
            {
                Id = id,
                Label = slider.Label,
                Description = slider.Description,
                Image = slider.Image,
            };

            return View(updateSliderVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSliderVM updateSliderVM)
        {

            if (!ModelState.IsValid)
                return View();

            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == updateSliderVM.Id);
            if (slider == null) return NotFound();

            if (updateSliderVM.Photo !=null)
            {
                if (updateSliderVM.Photo.ContentType.Contains("image/"))
                    return View();

                if (updateSliderVM.Photo.Length / 1024 > 500)
                    return View();


                string filename = updateSliderVM.Photo.FileName + " _ " + Guid.NewGuid().ToString();
                string path = Path.Combine(_environment.WebRootPath, "img", filename);

                using FileStream stream = new FileStream(path, FileMode.Create);

                await updateSliderVM.Photo.CopyToAsync(stream);

                string oldPath = Path.Combine(_environment.WebRootPath, "img", slider.Image);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
                slider.Image = filename;
            }

            slider.Label = updateSliderVM.Label;
            slider.Description = updateSliderVM.Description;
            slider.Percent = updateSliderVM.Percent;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

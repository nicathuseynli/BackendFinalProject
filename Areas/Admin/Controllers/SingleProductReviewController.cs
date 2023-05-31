using Backend_Final_Project.Areas.Admin.ViewModels.SingleProductPage;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SingleProductReviewController : Controller
    {
        private readonly AppDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public SingleProductReviewController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var singleproductreview = await _context.SingleProductReviews.ToListAsync();
            return View(singleproductreview);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSingleProductReviewVM createSingleProductReviewVM)
        {
            if (!ModelState.IsValid)
                return View();

            if (!createSingleProductReviewVM.UserPhoto.ContentType.Contains("image/"))
                return View();

            if (createSingleProductReviewVM.UserPhoto.Length / 1024 > 500)
                return View();

            string filename = Guid.NewGuid().ToString() + "_" + createSingleProductReviewVM.UserPhoto.FileName;

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

            using FileStream stream = new FileStream(path, FileMode.Create);

            await createSingleProductReviewVM.UserPhoto.CopyToAsync(stream);

            SingleProductReview singleProductReview = new()
            {
                CommentCount = createSingleProductReviewVM.CommentCount,
                UserFullname = createSingleProductReviewVM.UserFullname,
                UserComment = createSingleProductReviewVM.UserComment,
                UserImage = filename
            };
            await _context.SingleProductReviews.AddAsync(singleProductReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var singleProductReview = await _context.SingleProductReviews.FirstOrDefaultAsync(x => x.Id == id);
            if (singleProductReview == null)
                return NotFound();
            return View(singleProductReview);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var singleProductReview = await _context.SingleProductReviews.FirstOrDefaultAsync(x => x.Id == id);
            if (singleProductReview == null)
                return View();

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", singleProductReview.UserImage);

            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);

            System.IO.File.Delete(path);

            _context.SingleProductReviews.Remove(singleProductReview);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var singleProductReview = await _context.SingleProductReviews.FirstOrDefaultAsync(x => x.Id == id);
            if (singleProductReview == null) return NotFound();

            var updateSingleProductReviewVM = new UpdateSingleProductReviewVM()
            {
                Id = singleProductReview.Id,
                CommentCount = singleProductReview.CommentCount,
                UserComment = singleProductReview.UserComment,
                UserFullname = singleProductReview.UserFullname,
                UserImage = singleProductReview.UserImage,
            };
            return View(updateSingleProductReviewVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateSingleProductReviewVM updateSingleProductReviewVM)
        {

            var singleProductReview = await _context.SingleProductReviews.FirstOrDefaultAsync(x => x.Id == updateSingleProductReviewVM.Id);
            if (singleProductReview == null) return NotFound();

            if (updateSingleProductReviewVM.UserPhoto != null)
            {
                #region Create NewImage
                if (!updateSingleProductReviewVM.UserPhoto.ContentType.Contains("image/"))
                    return View();

                if (updateSingleProductReviewVM.UserPhoto.Length / 1024 > 1000)
                    return View();

                string filename = Guid.NewGuid().ToString() + " _ " + updateSingleProductReviewVM.UserPhoto.FileName;
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "images", filename);

                using FileStream stream = new FileStream(path, FileMode.Create);

                await updateSingleProductReviewVM.UserPhoto.CopyToAsync(stream);
                #endregion
                #region DeleteOldImage
                string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", singleProductReview.UserImage);
                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
                singleProductReview.UserImage = filename;
                #endregion

            }
            singleProductReview.CommentCount = updateSingleProductReviewVM.CommentCount;
            singleProductReview.UserFullname = updateSingleProductReviewVM.UserFullname;
            singleProductReview.UserComment = updateSingleProductReviewVM.UserComment;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

using Backend_Final_Project.Areas.Admin.ViewModels;
using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactInfoController : Controller
    {
        private readonly AppDbContext _context;

        public ContactInfoController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var contactInfo = await _context.ContactInformations.ToListAsync();
            return View(contactInfo);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateContactInfoVM createContactInfoVM)
        {
       
            ContactInfo contactInfo = new()
            {
                ContactDescription = createContactInfoVM.ContactDescription,
                MailAddress = createContactInfoVM.MailAddress,
                PhoneNumber = createContactInfoVM.PhoneNumber,
                Address = createContactInfoVM.Address,     
            };
            await _context.ContactInformations.AddAsync(contactInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var contactInfo = await _context.ContactInformations.FirstOrDefaultAsync(x => x.Id == id);
            if (contactInfo == null)
                return NotFound();
            return View(contactInfo);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            var contactInfo = await _context.ContactInformations.FirstOrDefaultAsync(x => x.Id == id);
            if (contactInfo == null)
                return View();

            _context.ContactInformations.Remove(contactInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var contactInfo = await _context.ContactInformations.FirstOrDefaultAsync(x => x.Id == id);
            if (contactInfo == null) return NotFound();

            var updatecontactInfoVM = new UpdateContactInfoVM()
            {
                Id = contactInfo.Id,
                ContactDescription = contactInfo.ContactDescription,
                MailAddress = contactInfo.MailAddress,
                PhoneNumber = contactInfo.PhoneNumber,
                Address = contactInfo.Address,
            };
            return View(updatecontactInfoVM);

        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateContactInfoVM updateContactInfoVM)
        {

            var contactInfo = await _context.ContactInformations.FirstOrDefaultAsync(x => x.Id == updateContactInfoVM.Id);
            if (contactInfo == null) return NotFound();

            contactInfo.ContactDescription = updateContactInfoVM.ContactDescription;
            contactInfo.MailAddress = updateContactInfoVM.MailAddress;
            contactInfo.PhoneNumber = updateContactInfoVM.PhoneNumber;
            contactInfo.Address = updateContactInfoVM.Address;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

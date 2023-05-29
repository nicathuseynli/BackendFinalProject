﻿using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Backend_Final_Project.ViewModels;
using Backend_Final_Project.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final_Project.Controllers
{
    public class LoginRegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _context;

        public LoginRegisterController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var loginAndRegister = await _context.UserLogins.ToListAsync();
            LoginRegisterVM loginVm = new()
            {
                registerVM =  ,            
            };
            return View(loginVm);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
                return View(loginVM);

            var appUser = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (appUser == null)
                 ModelState.AddModelError("","User Not Found");

            var result = await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, isPersistent:false,lockoutOnFailure:false);
             if (!result.Succeeded)
                ModelState.AddModelError("", "User Not Found");


            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) 
                return View(registerVM);

            AppUser newUser = new AppUser
            {
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Email = registerVM.Email,
            };

             await _userManager.CreateAsync(newUser, registerVM.Password);

            return RedirectToAction("Index", "Home");
        }

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return RedirectToAction("Index", "Home");
        //}


    }
}

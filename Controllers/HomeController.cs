﻿using Backend_Final_Project.Data;
using Backend_Final_Project.Models;
using Backend_Final_Project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Backend_Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.ToListAsync();
            var shippings = await _context.Shippings.ToListAsync();
            var homeproductimages= await _context.HomeProducts.ToListAsync();
            var blogs = await _context.Blogs.ToListAsync();
            var aboutcompanysliders = await _context.AboutCompanySliders.ToListAsync();
            var singleblog = await _context.Blogs.FirstOrDefaultAsync();
            var baner = await _context.Banners.ToListAsync();
            var testimonial = await _context.Testimonials.ToListAsync();
            var description = await _context.HomeDescriptions.FirstOrDefaultAsync();
            ViewBag.datetime = DateTime.Now.ToString("dd MMMM yyyy");

            HomeVM homevm = new HomeVM()
            {
                Blog = singleblog,
                Sliders = sliders,
                Shippings = shippings,
                HomeProducts = homeproductimages,
                Blogs = blogs,
                AboutCompanySliders = aboutcompanysliders,
                Testimonials = testimonial,
                Baners = baner,
                HomeDescriptions = description,
            };
            return View(homevm);
        }
    }
}
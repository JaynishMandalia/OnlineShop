using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Infrastructure;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class PagesController : Controller
    {
        private readonly OnlineShopContext context;

        public PagesController(OnlineShopContext context)
        {
            this.context = context;
        }

        //GET request   path => /admin/pages
        public async Task<IActionResult> Index()
        {
            IQueryable<Page> pages = from p in context.Pages orderby p.Sorting select p;
            List<Page> pagesList = await pages.ToListAsync();
            return View(pagesList);
        }
        
        //GET request   path => /admin/pages/details/5
        public async Task<IActionResult> Details(int id)
        {
            Page page = await context.Pages.FirstOrDefaultAsync(x => x.Id == id);
            if (page == null)
                return NotFound();

            return View(page);
        }
        //GET request   path => /admin/pages/create
        public IActionResult Create() => View();


        [HttpPost, ValidateAntiForgeryToken]
        //POST request   path => /admin/pages/create
        public async Task<IActionResult> Create(Page page)
        {
            if(ModelState.IsValid)
            {
                page.Slug = page.Title.ToLower().Replace(" ", "-");
                page.Sorting = 100;
                var slug = await context.Pages.FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if(slug != null)
                {
                    ModelState.AddModelError("", "Page Already Exists.");
                    return View(page);
                }
                context.Add(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The Page has been added!";
                return RedirectToAction("Index");
            }
            return View(page);
        }

        //GET request   path => /admin/pages/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Page page = await context.Pages.FindAsync(id);
            if (page == null)
                return NotFound();

            return View(page);
        }

        [HttpPost, ValidateAntiForgeryToken]
        //POST request   path => /admin/pages/create
        public async Task<IActionResult> Edit(Page page)
        {
            if (ModelState.IsValid)
            {
                page.Slug = page.Id == 1 ? "home" : page.Title.ToLower().Replace(" ", "-");

                var slug = await context.Pages.Where(x => x.Id != page.Id).FirstOrDefaultAsync(x => x.Slug == page.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Page Already Exists.");
                    return View(page);
                }
                context.Update(page);
                await context.SaveChangesAsync();
                TempData["Success"] = "The page has been edited!";
                return RedirectToAction("Edit", new { id = page.Id });
            }
            return View(page);
        }
    }
}

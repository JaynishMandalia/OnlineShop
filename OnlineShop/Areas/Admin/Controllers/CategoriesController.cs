using Microsoft.AspNetCore.Mvc;
using OnlineShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CategoriesController : Controller
    {
        private readonly OnlineShopContext context;
        public CategoriesController(OnlineShopContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

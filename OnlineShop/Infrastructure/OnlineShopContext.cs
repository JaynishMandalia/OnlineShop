using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Infrastructure
{
    public class OnlineShopContext : DbContext
    {
        public OnlineShopContext(DbContextOptions<OnlineShopContext> options) : base(options)
        {
        }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}

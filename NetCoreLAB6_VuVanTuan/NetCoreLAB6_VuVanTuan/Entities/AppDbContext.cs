using Microsoft.EntityFrameworkCore;
using NetCoreLAB6_VuVanTuan.Models;

namespace NetCoreLAB6_VuVanTuan.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}

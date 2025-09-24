using System.Data.Entity;  // Entity Framework 6
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Web_QLNT.Models
{
    // Sử dụng fully qualified name để tránh ambiguity
    public class AppDbContext : System.Data.Entity.DbContext
    {
        public AppDbContext() : base("name=DefaultConnection")
        {
        }

        // Sử dụng fully qualified name cho DbSet
        public System.Data.Entity.DbSet<SanPham> SanPhams { get; set; }
        public System.Data.Entity.DbSet<KhachHang> KhachHangs { get; set; }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
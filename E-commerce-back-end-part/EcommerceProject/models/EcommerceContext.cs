using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceProject.models
{
    public class EcommerceContext:IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext(DbContextOptions options):base(options)
        {
                
        }
        //public virtual DbSet<Order> Orders { get; set; }
        //public virtual DbSet<OrderDetials> Detials { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        //public virtual DbSet<Cart> Carts { get; set; }
        //public virtual DbSet<CartItems> Items { get; set; }
       



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<Product>()
          //.HasOne(p => p.Category)
          //.WithMany(b => b.Products)
          //.HasForeignKey(p => p.);
            //modelBuilder.Entity<OrderDetials>().HasKey(ww => new { ww.OrderId, ww.ProductId });
            //modelBuilder.Entity<CartItems>().HasKey(ww => new { ww.CartId, ww.productID });
            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=.;initial catalog=EcommerceProject;integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }
    }
}

using Fiorello_PB101_Demo.Demo.Models;
using Fiorello_PB101_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101_Demo.Data

{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<Setting> Settings { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Expert>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDeleted);

            modelBuilder.Entity<Setting>().HasData(
               new Setting
               {
                   Id = 1,
                   Key ="HeaderLogo",
                   Value = "logo.png",
                   SoftDeleted = false,
                   CreatedDate = DateTime.Now
               },
               new Setting
               {
                   Id = 2,
                   Key = "Phone",
                   Value = "233125",
                   SoftDeleted = false,
                   CreatedDate = DateTime.Now

               },
               new Setting
               {
                   Id = 3,
                   Key = "Address",
                   Value = "Ehmedli",
                   SoftDeleted = false,
                   CreatedDate = DateTime.Now
               }
           );



            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                    Id = 1,
                    Title = "Title1",
                    Description = "Reshadin blogu",
                    Image = "blog-feature-img-1.jpg",
                    CreatedDate = DateTime.Now
                },
                new Blog
                {
                    Id = 2,
                    Title = "Title2",
                    Description = "Ilqarin blogu",
                    Image = "blog-feature-img-3.jpg",
                    CreatedDate = DateTime.Now
                },
                new Blog
                {
                    Id = 3,
                    Title = "Title3",
                    Description = "Hacixanin blogu",
                    Image = "blog-feature-img-4.jpg",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}

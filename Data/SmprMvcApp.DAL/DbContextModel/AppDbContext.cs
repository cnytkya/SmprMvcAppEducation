using Microsoft.EntityFrameworkCore;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.DbContextModel
{
    public class AppDbContext : DbContext //Bizim veritabanına data aktarabilmemiz için DbContext sınıfını temel almamız gerekiyor. Çünkü ef.core'u temel aldığımız için aracı bir sınıfa ihtiyacımız var. bu da DbContext'tir. Bu sınıf üzerinden artık veritabanı işlemlerimizi gerçekleştirebiliyor olacağız.
    {
        //connection string'i bir parametre olarak alıp içeriye aktarmamız gerekiyor. bunun için kurucu metodu oluşturmamız gerekir.
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Burada yapılandırdığımız seçenekler DbContext'in temel sınıfına aktarılacaktır.
            //Veritabanına bağlanacağımızı bizim container'a yani Program.cs'e söylememiz gerekiyor.
            //builder.Services.AddDbContext<AppDbContext>(); => Program.cs'e Dipendency Injection uygulayarak uygulamanın veritabanını ile iletişimini sağladık.
        }

        //Hazır kayıt(seed data)

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Teknoloji", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Kitap", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Tekstil", DisplayOrder = 3 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "HP Victus",
                    Author = "Cüneyt",
                    Description = "HP Victus 16-R1001NT Intel Core i7 14700HX 16 GB 1 TB SSD",
                    ISBN = "ASS231D65F45DF",
                    Price = 55,
                    ListPrice = 44,
                    Price50 = 55,
                    Price100 = 255,
                    CategoryId = 1,
                    ImageUrl = "",
                },
                new Product
                {
                    Id = 2,
                    Title = "Lenova Thingpad",
                    Author = "Kasım",
                    Description = "LENOVO Thinkpad Z16 Gen 1 Ryzen 9 Pro 6950h 32gb 1tb Ssd...",
                    ISBN = "ASS231D65F45DF",
                    Price = 250,
                    ListPrice = 2445,
                    Price50 = 250,
                    Price100 = 2522,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Lenova Thingpad",
                    Author = "Kasım",
                    Description = "LENOVO Thinkpad Z16 Gen 1 Ryzen 9 Pro 6950h 32gb 1tb Ssd...",
                    ISBN = "ASS231D65F45DF",
                    Price = 25,
                    ListPrice = 250,
                    Price50 = 23,
                    Price100 = 25,
                    CategoryId = 3,
                    ImageUrl = ""
                });
        }
    }
}
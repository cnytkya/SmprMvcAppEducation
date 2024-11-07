using Microsoft.EntityFrameworkCore;
using SmprMvcApp.Models;

namespace SmprMvcApp.Data
{
    public class AppDbContext : DbContext //Bizim veritabanına data aktarabilmemiz için DbContext sınıfını temel almamız gerekiyor. Çünkü ef.core'u temel aldığımız için aracı bir sınıfa ihtiyacımız var. bu da DbContext'tir. Bu sınıf üzerinden artık veritabanı işlemlerimizi gerçekleştirebiliyor olacağız.
    {
        //connection string'i bir parametre olarak alıp içeriye aktarmamız gerekiyor. bunun için kurucu metodu oluşturmamız gerekir.
        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Burada yapılandırdığımız seçenekler DbContext'in temel sınıfına aktarılacaktır.
            //Veritabanına bağlanacağımızı bizim container'a yani Program.cs'e söylememiz gerekiyor.
            //builder.Services.AddDbContext<AppDbContext>(); => Program.cs'e Dipendency Injection uygulayarak uygulamanın veritabanını ile iletişimini sağladık.
        }

        //Hazır kayıt(seed data)

        /*        protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                    modelBuilder.Entity<Category>().HasData(
                        new Category { Id = 1, Name = "Teknoloji", DisplayOrder = 1 },
                        new Category { Id = 2, Name = "Kitap", DisplayOrder = 2 },
                        new Category { Id = 3, Name = "Tekstil", DisplayOrder = 3 }
                        );
                }*/
    }
}
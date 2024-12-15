using SmprMvcApp.DAL.DbContextModel;

// Veritabanı bağlamı için gerekli olan `AppDbContext` sınıfını kullanabilmek için bu namespace dahil ediliyor.

using SmprMvcApp.DAL.Repository.Interface;

// Repository interface'lerinin (örneğin `ICategoryRepository`, `IProductRepository`, `IUnitOfWork`) erişilebilir olması için bu namespace kullanılıyor.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Temel C# kütüphanelerini ve görevlerle (Task) çalışmak için gerekli sınıfları sağlar.

namespace SmprMvcApp.DAL.Repository.Concrete
// Bu namespace, interface'lerin somut (concrete) uygulamalarını içerir.
// Örneğin, `UnitOfWork` ve belirli repository sınıfları gibi.
{
    public class UnitOfWork : IUnitOfWork
    // `UnitOfWork` sınıfı, `IUnitOfWork` arayüzünü uygular ve veritabanı işlemlerini bir bütün olarak yönetmek için kullanılır.
    {
        private AppDbContext _appDbContext;
        // Veritabanı bağlamını temsil eden özel bir alan.
        // Veritabanı işlemlerini gerçekleştirmek için kullanılır.

        public ICategoryRepository Category { get; private set; }
        // Kategori ile ilgili veritabanı işlemlerini yöneten repository.
        // `ICategoryRepository` arayüzünü uygular.

        public IProductRepository Product { get; private set; }

        // Ürün ile ilgili veritabanı işlemlerini yöneten repository.
        // `IProductRepository` arayüzünü uygular.

        public UnitOfWork(AppDbContext context)
        // `UnitOfWork` sınıfının yapıcı metodu. Veritabanı bağlamını alır ve repository'leri başlatır.
        {
            _appDbContext = context;
            // Yapıcı metodun aldığı `context` parametresi, `_appDbContext` alanına atanır.

            Category = new CategoryRepository(context);
            // `CategoryRepository` sınıfının bir örneği oluşturulup `Category` alanına atanır.

            Product = new ProductRepository(context);
            // `ProductRepository` sınıfının bir örneği oluşturulup `Product` alanına atanır.
        }

        public void Save()
        // Tüm yapılan değişiklikleri veritabanına kaydeden metod.
        {
            _appDbContext.SaveChanges();
            //`_appDbContext` aracılığıyla tüm değişiklikler veritabanına yazılır.
        }
    }
}
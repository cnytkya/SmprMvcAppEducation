using Microsoft.EntityFrameworkCore;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IAppUserRepository AppUser { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        void Save();//Tüm yapılan değişiklikler veritabanına kaydedilir. Eğer bu işlem sırasında bir hata oluşursa, tüm işlemler geri alınır (rollback).
    }
}

//SaveChanges gibi işlemleri merkezi bir noktadan yöneterek, farklı işlemlerin birbirini etkilememesini sağlar.
//Transaction Yönetimi
//Birden fazla repository ile çalışırken aynı veritabanı işlemi için bir transaction oluşturmanıza olanak tanır.
//Tüm işlemler tek bir transaction altında toplanır, bu da işlemlerden biri başarısız olduğunda tüm işlemleri geri alarak veri tutarlılığını sağlar.

//Uygulamada birden fazla repository varsa, IUnitOfWork bu repository'ler arasında koordinasyon sağlar. Yani tek bir transaction ile tüm repository'leri yönetmemize olanak tanır.
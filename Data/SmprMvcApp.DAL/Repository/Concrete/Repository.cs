using Microsoft.EntityFrameworkCore;
using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using System.Linq.Expressions;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : class //Implement Interface, T burda herhangi bir model'i temsil eder.
    {
        //veritabanı işlemleri için veritabanı bağlamını buraya injecte etmemiz gerekir. Dedependency Injection
        private readonly AppDbContext _appDbContext;

        internal DbSet<T> dbSet;

        public Repository(AppDbContext context)//kurucu metot
        {
            _appDbContext = context;
            this.dbSet = _appDbContext.Set<T>(); //_context.Categories == dbSet;
            //_context.Categories.Add(); => dbSet.Add();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
            //Category? category3 = _appDbContext.Categories.Where(c => c.Id == id).FirstOrDefault(); => controllerda yaptığımız query'nin aynısını bu metot yapacak.
        }

        public IEnumerable<T> GetAll()//bütün T türündeki nesneleri listeler
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            //Metot bir koleksiyon (IEnumerable<T>) alır. T generik bir tiptir, bu da metot çağrıldığında hangi tipte bir koleksiyonun işleneceğini belirler.
            //void: Metodun bir dönüş değeri yok. Bu, metot çağrıldığında herhangi bir değer döndürmeyeceği anlamına gelir. O yzüden return demeyeceğiz.,
            dbSet.RemoveRange(entity);//RemoveRange: Metodun adı, birden fazla öğeyi kaldırmayı amaçladığını gösterir.
        }
    }
}
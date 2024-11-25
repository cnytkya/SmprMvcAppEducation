using System.Linq.Expressions;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        //T=>Category
        IEnumerable<T> GetAll(string? includeProperties = null);

        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        void Add(T entity);

        //void Update(T entity);

        //void Save(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entity);
    }
}
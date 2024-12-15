using SmprMvcApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company entity);

        //void Save();

        //IRepository içinde tanımlanan tüm metotlar otomatik olarak ICompanyRepository'ye dahil olur. Eğer generic bir yapı kurmasaydık her bir model için aşağıdaki gibi metotlar oluşturmak gerekird. DRY(Dont Repeat Yourself) prensibine aykırı bir durum olurdu.

        //1. DRY Prensibi Nedir?: DRY(Don't Repeat Yourself) yazılım geliştirmede bir prensiptir ve aynı kod veya mantığın birden fazla yerde tekrar edilmesinden kaçınmayı hedefler.
        //aşağıdaki metotları generic bir yapıya çevirip bütün modeller için kullanabileceğiz. Bunu genellikle IRepository adında bir arayüzde yazarız ve istediğimiz yerde bunu imlement(uygulamak) edebiliriz.
        /*

                  IEnumerable<Company> GetAll();

                  Company Get(Expression<Func<Company, bool>> filter);

                  void Add(Company entity);

                  void Update(Company entity);

                  void Save(Company entity);

                  void Remove(Company entity);

                  void RemoveRange(IEnumerable<Company> entity);

         */
    }
}
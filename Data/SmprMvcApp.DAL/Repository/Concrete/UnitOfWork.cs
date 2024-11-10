using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _appDbContext;
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            _appDbContext = context;
            Category = new CategoryRepository(context);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }
}
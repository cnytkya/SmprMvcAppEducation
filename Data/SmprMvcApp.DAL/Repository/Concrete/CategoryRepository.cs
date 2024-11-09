using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }

        public void Save(Category entity)
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Category entity)
        {
            _appDbContext.Update(entity);
        }
    }
}
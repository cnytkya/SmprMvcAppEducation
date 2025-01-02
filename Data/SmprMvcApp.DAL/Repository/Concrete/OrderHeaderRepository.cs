using SmprMvcApp.DAL.DbContextModel;
using SmprMvcApp.DAL.Repository.Interface;
using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Concrete
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private AppDbContext _appDbContext;

        public OrderHeaderRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public void Update(OrderHeader entity)
        {
            _appDbContext.OrderHeaders.Update(entity);
        }
    }
}
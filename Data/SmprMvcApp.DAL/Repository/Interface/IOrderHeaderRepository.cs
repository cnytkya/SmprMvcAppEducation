using SmprMvcApp.EntityLayer.Entities;

namespace SmprMvcApp.DAL.Repository.Interface
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader entity);
    }
}
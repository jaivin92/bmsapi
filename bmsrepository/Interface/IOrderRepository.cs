using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IOrderRepository
    {
        Task<bool> IsExists(OrderModel orderModel);
        Task Insert(OrderModel orderModel);
        Task Update(OrderModel orderModel);
        Task<OrderModel?> GetById(long id);
        Task<List<OrderModel>> GetAll(OrderModel model);
    }
}

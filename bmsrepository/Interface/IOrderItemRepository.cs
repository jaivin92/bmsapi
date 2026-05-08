using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IOrderItemRepository
    {
        Task<bool> IsExists(OrderItemModel orderItemModel);
        Task Insert(OrderItemModel orderItemModel);
        Task Update(OrderItemModel orderItemModel);
        Task<OrderItemModel?> GetById(long id);
        Task<List<OrderItemModel>> GetAll(OrderItemModel model);
    }
}

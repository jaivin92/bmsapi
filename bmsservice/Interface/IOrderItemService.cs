using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IOrderItemService
    {
        Task Insert(OrderItemModel orderItemModel);
        Task Update(OrderItemModel orderItemModel);
        Task<OrderItemModel> GetById(long id);
        Task<List<OrderItemModel>> GetAll(DataTableRequestModel dataTableRequestModel);
        Task<OrderItemModel> GetSingle(DataTableRequestModel dataTableRequestModel);
    }
}

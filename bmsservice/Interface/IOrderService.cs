using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IOrderService
    {
        Task Insert(OrderModel orderModel);
        Task Update(OrderModel orderModel);
        Task<OrderModel> GetById(long id);
        Task<List<OrderModel>> GetAll(DataTableRequestModel dataTableRequestModel);
        Task<OrderModel> GetSingle(DataTableRequestModel dataTableRequestModel);
    }
}

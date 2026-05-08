using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IFoodTableService
    {
        Task Insert(FoodTableModel foodTableModel);
        Task Update(FoodTableModel foodTableModel);
        Task<FoodTableModel> GetById(long id);
        Task<List<FoodTableModel>> GetAll(DataTableRequestModel dataTableRequestModel);
        Task<FoodTableModel> GetSingle(DataTableRequestModel dataTableRequestModel);
    }
}

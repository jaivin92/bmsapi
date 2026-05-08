using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IFoodService
    {
        Task Insert(FoodModel foodModel);
        Task Update(FoodModel foodModel);
        Task<FoodModel> GetById(long id);
        Task<List<FoodModel>> GetAll(DataTableRequestModel dataTableRequestModel);
        Task<FoodModel> GetSingle(DataTableRequestModel dataTableRequestModel);
    }
}

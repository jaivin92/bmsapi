using bmsmodel.Common;

namespace bmsservice.Interface
{
    public interface IFoodCategoryService
    {
        Task Insert(FoodCategoryModel foodCategoryModel);
        Task Update(FoodCategoryModel foodCategoryModel);
        Task<FoodCategoryModel> GetById(long id);
        Task<List<FoodCategoryModel>> GetAll(DataTableRequestModel dataTableRequestModel);
        Task<FoodCategoryModel> GetSingle(DataTableRequestModel dataTableRequestModel);
    }
}

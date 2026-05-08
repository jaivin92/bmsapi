using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IFoodCategoryRepository
    {
        Task<bool> IsExists(FoodCategoryModel foodCategoryModel);
        Task Insert(FoodCategoryModel foodCategoryModel);
        Task Update(FoodCategoryModel foodCategoryModel);
        Task<FoodCategoryModel?> GetById(long id);
        Task<List<FoodCategoryModel>> GetAll(FoodCategoryModel model);
    }
}

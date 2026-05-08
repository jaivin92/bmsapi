using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IFoodRepository
    {
        Task<bool> IsExists(FoodModel foodModel);
        Task Insert(FoodModel foodModel);
        Task Update(FoodModel foodModel);
        Task<FoodModel?> GetById(long id);
        Task<List<FoodModel>> GetAll(FoodModel model);
    }
}

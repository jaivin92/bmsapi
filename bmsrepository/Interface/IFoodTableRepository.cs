using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IFoodTableRepository
    {
        Task<bool> IsExists(FoodTableModel foodTableModel);
        Task Insert(FoodTableModel foodTableModel);
        Task Update(FoodTableModel foodTableModel);
        Task<FoodTableModel?> GetById(long id);
        Task<List<FoodTableModel>> GetAll(FoodTableModel model);
    }
}

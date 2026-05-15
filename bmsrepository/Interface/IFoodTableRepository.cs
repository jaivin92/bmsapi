using bmslib.Enmus;
using bmsmodel.Common;

namespace bmsrepository.Interface
{
    public interface IFoodTableRepository
    {
        Task<bool> IsExists(FoodTableModel foodTableModel);
        Task Insert(FoodTableModel foodTableModel);
        Task Update(FoodTableModel foodTableModel);
        Task UpdateTableStatus(long tableId, FoodTableType tableStatus);
        Task ReleaseCleaningTablesOlderThan(int minutes);
        Task<FoodTableModel?> GetById(long id);
        Task<List<FoodTableModel>> GetAll(FoodTableModel model);
    }
}

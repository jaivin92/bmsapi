using bmslib;
using bmslib.Exceptions;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class FoodTableService(IFoodTableRepository foodTableRepository) : IFoodTableService
    {
        private IFoodTableRepository _foodTableRepository = foodTableRepository;

        public async Task Insert(FoodTableModel foodTableModel)
        {
            if (!await _foodTableRepository.IsExists(foodTableModel))
            {
                await _foodTableRepository.Insert(foodTableModel);
            }
            else
            {
                throw new ValidationException("Food Table".AlreadyExist());
            }
        }

        public async Task Update(FoodTableModel foodTableModel)
        {
            if (!await _foodTableRepository.IsExists(foodTableModel))
            {
                await _foodTableRepository.Update(foodTableModel);
            }
            else
            {
                throw new ValidationException("Food Table".AlreadyExist());
            }
        }

        public async Task<FoodTableModel> GetById(long id)
        {
            return await _foodTableRepository.GetById(id);
        }

        public async Task<List<FoodTableModel>> GetAll(DataTableRequestModel dataTableRequestModel)
        {
            FoodTableModel _foodTableModel = new();
            if (dataTableRequestModel != null)
            {
                _foodTableModel = dataTableRequestModel.FilterObj.GetModel<FoodTableModel>();
                _foodTableModel.DataTableRequestModel = dataTableRequestModel;
            }
            return await _foodTableRepository.GetAll(_foodTableModel);
        }

        public async Task<FoodTableModel> GetSingle(DataTableRequestModel dataTableRequestModel)
        {
            FoodTableModel _foodTableModel = new();
            if (dataTableRequestModel != null)
            {
                _foodTableModel = dataTableRequestModel.FilterObj.GetModel<FoodTableModel>();
                _foodTableModel.DataTableRequestModel = dataTableRequestModel;
            }
            var result = await _foodTableRepository.GetAll(_foodTableModel);
            return result.FirstOrDefault();
        }
    }
}

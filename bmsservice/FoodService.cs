using bmslib;
using bmslib.Exceptions;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class FoodService(IFoodRepository foodRepository) : IFoodService
    {
        private IFoodRepository _foodRepository = foodRepository;

        public async Task Insert(FoodModel foodModel)
        {
            if (!await _foodRepository.IsExists(foodModel))
            {
                await _foodRepository.Insert(foodModel);
            }
            else
            {
                throw new ValidationException("Food".AlreadyExist());
            }
        }

        public async Task Update(FoodModel foodModel)
        {
            if (!await _foodRepository.IsExists(foodModel))
            {
                await _foodRepository.Update(foodModel);
            }
            else
            {
                throw new ValidationException("Food".AlreadyExist());
            }
        }

        public async Task<FoodModel> GetById(long id)
        {
            return await _foodRepository.GetById(id);
        }

        public async Task<List<FoodModel>> GetAll(DataTableRequestModel dataTableRequestModel)
        {
            FoodModel _foodModel = new();
            if (dataTableRequestModel != null)
            {
                _foodModel = dataTableRequestModel.FilterObj.GetModel<FoodModel>();
                _foodModel.DataTableRequestModel = dataTableRequestModel;
            }
            return await _foodRepository.GetAll(_foodModel);
        }

        public async Task<FoodModel> GetSingle(DataTableRequestModel dataTableRequestModel)
        {
            FoodModel _foodModel = new();
            if (dataTableRequestModel != null)
            {
                _foodModel = dataTableRequestModel.FilterObj.GetModel<FoodModel>();
                _foodModel.DataTableRequestModel = dataTableRequestModel;
            }
            var result = await _foodRepository.GetAll(_foodModel);
            return result.FirstOrDefault();
        }
    }
}

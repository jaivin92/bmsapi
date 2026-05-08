using bmslib;
using bmslib.Exceptions;
using bmslib.Resource;
using bmsmodel.Common;
using bmsrepository.Interface;
using bmsservice.Interface;

namespace bmsservice
{
    internal class FoodCategoryService(IFoodCategoryRepository foodCategoryRepository) : IFoodCategoryService
    {
        private IFoodCategoryRepository _foodCategoryRepository = foodCategoryRepository;

        public async Task Insert(FoodCategoryModel foodCategoryModel)
        {
            if (!await _foodCategoryRepository.IsExists(foodCategoryModel))
            {
                await _foodCategoryRepository.Insert(foodCategoryModel);
            }
            else
            {
                throw new ValidationException("Food Category".AlreadyExist());
            }
        }

        public async Task Update(FoodCategoryModel foodCategoryModel)
        {
            if (!await _foodCategoryRepository.IsExists(foodCategoryModel))
            {
                await _foodCategoryRepository.Update(foodCategoryModel);
            }
            else
            {
                throw new ValidationException("Food Category".AlreadyExist());
            }
        }

        public async Task<FoodCategoryModel> GetById(long id)
        {
            return await _foodCategoryRepository.GetById(id);
        }

        public async Task<List<FoodCategoryModel>> GetAll(DataTableRequestModel dataTableRequestModel)
        {
            FoodCategoryModel _foodCategoryModel = new();
            if (dataTableRequestModel != null)
            {
                _foodCategoryModel = dataTableRequestModel.FilterObj.GetModel<FoodCategoryModel>();
                _foodCategoryModel.DataTableRequestModel = dataTableRequestModel;
            }
            return await _foodCategoryRepository.GetAll(_foodCategoryModel);
        }

        public async Task<FoodCategoryModel> GetSingle(DataTableRequestModel dataTableRequestModel)
        {
            FoodCategoryModel _foodCategoryModel = new();
            if (dataTableRequestModel != null)
            {
                _foodCategoryModel = dataTableRequestModel.FilterObj.GetModel<FoodCategoryModel>();
                _foodCategoryModel.DataTableRequestModel = dataTableRequestModel;
            }
            var result = await _foodCategoryRepository.GetAll(_foodCategoryModel);
            return result.FirstOrDefault();
        }
    }
}

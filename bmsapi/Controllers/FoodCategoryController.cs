using bmsapi.Helpers;
using bmslib.Resource;
using bmsmodel.Common;
using bmsservice.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : BaseController
    {
        private IFoodCategoryService _foodCategoryService;
        public FoodCategoryController(IFoodCategoryService foodCategoryService)
        {
            _foodCategoryService = foodCategoryService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] FoodCategoryModel model)
        {
            await _foodCategoryService.Insert(model);
            return await Success(model, message: "Food Category".InsertedSuccessfully());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FoodCategoryModel model)
        {
            await _foodCategoryService.Update(model);
            return await Success(model, message: "Food Category".UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var foodCategory = await _foodCategoryService.GetById(id);
            return await Success(foodCategory);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            var foodCategories = await _foodCategoryService.GetAll(dataTableRequestModel);
            return await Success(foodCategories.Datatable());
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            return await Success(await _foodCategoryService.GetSingle(dataTableRequestModel));
        }
    }
}

using bmsapi.Helpers;
using bmslib.Resource;
using bmsmodel.Common;
using bmsservice.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : BaseController
    {
        private IFoodService _foodService;
        public FoodController(IFoodService foodService)
        {
            _foodService = foodService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] FoodModel model)
        {
            await _foodService.Insert(model);
            return await Success(model, message: "Food".InsertedSuccessfully());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FoodModel model)
        {
            await _foodService.Update(model);
            return await Success(model, message: "Food".UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var food = await _foodService.GetById(id);
            return await Success(food);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            var foods = await _foodService.GetAll(dataTableRequestModel);
            return await Success(foods.Datatable());
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            return await Success(await _foodService.GetSingle(dataTableRequestModel));
        }
    }
}

using bmsapi.Helpers;
using bmslib.Resource;
using bmsmodel.Common;
using bmsservice.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTableController : BaseController
    {
        private IFoodTableService _foodTableService;
        public FoodTableController(IFoodTableService foodTableService)
        {
            _foodTableService = foodTableService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] FoodTableModel model)
        {
            await _foodTableService.Insert(model);
            return await Success(model, message: "Food Table".InsertedSuccessfully());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] FoodTableModel model)
        {
            await _foodTableService.Update(model);
            return await Success(model, message: "Food Table".UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var foodTable = await _foodTableService.GetById(id);
            return await Success(foodTable);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            var foodTables = await _foodTableService.GetAll(dataTableRequestModel);
            return await Success(foodTables.Datatable());
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            return await Success(await _foodTableService.GetSingle(dataTableRequestModel));
        }
    }
}

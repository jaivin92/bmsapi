using bmslib.Resource;
using bmsmodel.Common;
using bmsservice.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : BaseController
    {
        private IOrderItemService _orderItemService;
        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] OrderItemModel model)
        {
            await _orderItemService.Insert(model);
            return await Success(model, message: "Order Item".InsertedSuccessfully());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrderItemModel model)
        {
            await _orderItemService.Update(model);
            return await Success(model, message: "Order Item".UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var orderItem = await _orderItemService.GetById(id);
            return await Success(orderItem);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            var orderItems = await _orderItemService.GetAll(dataTableRequestModel);
            return await Success(orderItems);
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            return await Success(await _orderItemService.GetSingle(dataTableRequestModel));
        }
    }
}

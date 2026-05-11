using bmsapi.Helpers;
using bmslib.Resource;
using bmsmodel.Common;
using bmsservice.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] OrderModel model)
        {
            await _orderService.Insert(model);
            return await Success(model, message: "Order".InsertedSuccessfully());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrderModel model)
        {
            await _orderService.Update(model);
            return await Success(model, message: "Order".UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var order = await _orderService.GetById(id);
            return await Success(order);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            var orders = await _orderService.GetAll(dataTableRequestModel);
            return await Success(orders.Datatable());
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            return await Success(await _orderService.GetSingle(dataTableRequestModel));
        }
    }
}

using bmsapi.Helpers;
using bmslib.Resource;
using bmsmodel.Common;
using bmsservice.Interface;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody] UserModel model)
        {
            await _userService.Insert(model);
            return await Success(model, message: SystemMessages.User.InsertedSuccessfully());
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            await _userService.Update(model);
            return await Success(model, message: SystemMessages.User.UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userService.GetById(id);
            return await Success(user);
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            var users = await _userService.GetAll(dataTableRequestModel);
            return await Success(users.Datatable());
        }

        [HttpPost("GetSingle")]
        public async Task<IActionResult> GetSingle([FromBody] DataTableRequestModel dataTableRequestModel)
        {
            return await Success(await _userService.GetSingle(dataTableRequestModel));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            var user = await _userService.Login(model);
            return await Success(user);
        }
    }
}

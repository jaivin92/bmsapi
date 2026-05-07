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
            if (!await _userService.Insert(model))
            {
                return await Error(SystemMessages.User.AlreadyExist());
            }

            return await Success(model, message: SystemMessages.User.InsertedSuccessfully());
        }

        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] UserModel model)
        {
            if (!await _userService.Update(model))
            {
                return await Error(SystemMessages.User.AlreadyExist());
            }

            return await Success(model, message: SystemMessages.User.UpdatedSuccessfully());
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var user = await _userService.GetById(id);
            return await Success(user);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return await Success(users);
        }


    }
}

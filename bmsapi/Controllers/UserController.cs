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


    }
}

using bmsapi.Helpers;
using bmslib.Resource;
using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Controllers
{

    public class BaseController : ControllerBase
    {
        [NonAction]
        public async Task<IActionResult> Success<T>(T data, string message = "", bool isShowNoData = true)
        {
            if (data == null && isShowNoData)
            {
                return await Error(SystemMessages.NoDataFound);
            }
            else
            {
                var _result = new Success<T>
                {
                    Status = true,
                    Data = data,
                    DataVersion = "1",
                    Message = message
                };

                return Ok(_result);
            }
        }

        [NonAction]
        public async Task<IActionResult> Error(string message, bool isException = false)
        {
            var myError = new Error
            {
                Status = false,
                Message = message
            };

            //if (!isException)
            //    Util.Log.Error(Request.HttpContext.TraceIdentifier + "--" + message);

            return BadRequest(myError);
        }
    }
}

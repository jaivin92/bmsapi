using Microsoft.AspNetCore.Mvc;

namespace bmsapi.Helpers
{
    public class SuccessResult<T> : IActionResult
    {
        Success<T> _success;

        public SuccessResult(Success<T> success)
        {
            _success = success;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_success);
            await objectResult.ExecuteResultAsync(context);
        }
    }

    public class Success<T>
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public string DataVersion { get; set; }
    }
}

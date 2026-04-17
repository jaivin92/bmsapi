using bmslib.Enmus;
using Microsoft.AspNetCore.Mvc;
namespace bmsapi.Helpers
{
    public class ErrorResult : IActionResult
    {
        Error _error;

        public ErrorResult(Error error)
        {
            _error = error;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var objectResult = new ObjectResult(_error);
            await objectResult.ExecuteResultAsync(context);
        }
    }

    public class Error
    {
        public bool Status { get; set; }

        public ErrorType ErrorType { get; set; }

        public string Message { get; set; }
    }
}

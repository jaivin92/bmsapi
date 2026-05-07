using bmslib.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace bmsapi.Helpers.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException)
            {
                var _errorObj = new Error()
                {
                    Status = false,
                    ErrorType = bmslib.Enmus.ErrorType.Validation,
                    Message = context.Exception.Message
                };
                context.Result = new ObjectResult(_errorObj)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if (context.Exception is Exception)
            {
                var _errorObj = new Error()
                {
                    Status = false,
                    ErrorType = bmslib.Enmus.ErrorType.Exception,
                    Message = context.Exception.Message
                };

                context.Result = new ObjectResult(_errorObj)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };

                context.ExceptionHandled = true;
            }
        }
    }
}

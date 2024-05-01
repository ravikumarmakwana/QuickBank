using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Exceptions;

namespace QuickBank.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(ExceptionContext context)
        {
            await base.OnExceptionAsync(context);

            var exception = context.Exception;

            if (exception is InvalidOperationException || exception is QuickBankException)
            {
                context.Result = new ObjectResult(exception.Message)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                context.Result = new ObjectResult("Unexpected error occurred while processing request.")
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        }
    }
}

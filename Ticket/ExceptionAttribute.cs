using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Ticket.Core;

namespace Ticket
{
    public class ExceptionAttribute : IExceptionFilter
    {
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionAttribute(IHostEnvironment hostEnvironment) =>
            _hostEnvironment = hostEnvironment;

        public void OnException(ExceptionContext context)
        {
            var baseException = context.Exception.GetBaseException();

            if (baseException is BusinessException bex)
            {
                switch (bex.ErrorCode)
                {
                    case BusinessErrorCode.LoginFail:
                    case BusinessErrorCode.NoPermission:
                        context.HttpContext.Response.StatusCode = 403;
                        context.HttpContext.Response.WriteAsync(baseException.Message);
                        break;
                    default:
                        break;
                }
            }


            if (!_hostEnvironment.IsDevelopment())
            {
                // Don't display exception details unless running in Development.
                return;
            }

            //context.Result = new ContentResult
            //{
            //    Content = context.Exception.ToString()
            //};
        }
    }
}

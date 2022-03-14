using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Core;

namespace TicketAPI
{
    public class ExceptionAttribute : ActionFilterAttribute, IExceptionFilter
    {
        public  void OnException(ExceptionContext context)
        {
            var baseEx = context.Exception.GetBaseException();

            if (baseEx is BusinessException bex)
            {
                switch (bex.ErrorCode)
                {
                    case BusinessErrorCode.NoPermission:

                        break;
                    default:
                        break;
                }
            }
        }
    }
}

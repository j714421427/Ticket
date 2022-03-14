using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TicketAPI
{
    public class ParameterBindingAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var arguments = actionContext.ActionDescriptor;



            base.OnActionExecuting(actionContext);
        }
    }
}

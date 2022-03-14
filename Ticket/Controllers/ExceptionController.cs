using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.Controllers
{
    [TypeFilter(typeof(ExceptionAttribute))]
    public class ExceptionController : Controller
    {
        //public IActionResult Index() =>
        //    Content($"- {nameof(ExceptionController)}.{nameof(Index)}");
    }
}

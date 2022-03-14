using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Client.TicketTask;
using Ticket.ViewModel.TickeyTask;

namespace Ticket.Controllers
{
    public class TicketTaskController : ExceptionController
    {
        public readonly TicketTaskClient _TicketTaskClient;

        public TicketTaskController(TicketTaskClient ticketTaskClient)
        {
            _TicketTaskClient = ticketTaskClient;
        }
        public IActionResult Main()
        {
            return View();
        }

        public IActionResult Detail(int id, string mode)
        {
            ViewBag.Mode = mode;
            ViewBag.Id = id;
            return View();
        }

        public async Task<TicketTaskMain> GetAll(TicketTaskSearch query)
        {
            var result = await _TicketTaskClient.Search(query);

            return result;
        }

        public async Task<TicketTaskDetail> Get(int id)
        {
            var result = await _TicketTaskClient.FindById(id);

            return result;
        }

        public async Task Save(TicketTaskDetail model)
        {
            if (model.Id == 0)
            {
                var result = await _TicketTaskClient.Create(model);
            }
            else
            {
                var result = await _TicketTaskClient.Update(model);
            }

            return;
        }

        public async Task Resolve(int id)
        {
            var result = await _TicketTaskClient.Resolve(id);

            return;
        }

        public async Task Delete(int id)
        {
            var result = await _TicketTaskClient.Delete(id);

            return;
        }
    }
}

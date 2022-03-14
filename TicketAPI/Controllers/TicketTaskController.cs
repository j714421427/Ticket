using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ticket.Client;
using Ticket.Model;
using Ticket.Model.Enums;
using Ticket.Service;
using Ticket.ViewModel.TickeyTask;

namespace TicketAPI.Controllers
{
    [Route(WebApiControllerRoutePrefixes.TicketTasks)]
    public class TicketTaskController : ExceptionController
    {
        public readonly TicketTaskService _TicketTaskService;
        public TicketTaskController(TicketContext context, TicketTaskService ticketTaskService)
        {
            _TicketTaskService = ticketTaskService;
        }

        [HttpGet("getall")]
        [CustomAuthorization(PermissionCode.TicketTask_Access)]
        public TicketTaskMain GetAll(TicketTaskSearch search)
        {
            var result = new TicketTaskMain();

            var query = BulidQuery(search);

            result.Items = _TicketTaskService.GetAllQuery(query).Select(s => new TicketTaskItem
            {
                Id = s.Id,
                Title = s.Title,
                TaskType = s.TicketTaskType.ToString(),
                TaskStatus = s.TicketTaskStatus.ToString(),
            }).ToList();

            return result;
        }

        [HttpGet]
        [CustomAuthorization(PermissionCode.TicketTask_Access)]
        public TicketTaskDetail Get(int id)
        {
            var entity = _TicketTaskService.GetById(id);

            var result = new TicketTaskDetail
            {
                Id = entity.Id,
                Title = entity.Title,
                Content = entity.Content,
                TaskType = entity.TicketTaskType,
                TaskStatus = entity.TicketTaskStatus,
            };

            return result;
        }

        [HttpPost]
        [CustomAuthorization(PermissionCode.TicketTask_Edit)]
        public void Post([FromBody] TicketTaskDetail model)
        {
            var item = new TicketTack()
            {
                Title = model.Title,
                Content = model.Content,
                TicketTaskType = model.TaskType,
                TicketTaskStatus = model.TaskStatus,
                Status = EntityStatus.Enabled
            };

            _TicketTaskService.Update(item);

            return;
        }

        [HttpPut]
        [CustomAuthorization(PermissionCode.TicketTask_Edit)]
        public void Put([FromBody] TicketTaskDetail model)
        {
            var item = new TicketTack()
            {
                Id = model.Id,
                Title = model.Title,
                Content = model.Content,
                TicketTaskType = model.TaskType,
                TicketTaskStatus = model.TaskStatus,
            };

            _TicketTaskService.Update(item);

            return;
        }

        [HttpPatch]
        [CustomAuthorization(PermissionCode.TicketTask_Resolve)]
        public void Patch(int id)
        {
            _TicketTaskService.Resolve(id);

            return;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _TicketTaskService.UpdateEntityStatus(id, EntityStatus.Deprecated);

            return;
        }



        private IEnumerable<Expression<Func<TicketTack, bool>>> BulidQuery(TicketTaskSearch model)
        {
            var expressions = new List<Expression<Func<TicketTack, bool>>>();

            if (!string.IsNullOrEmpty(model.Title))
            {
                expressions.Add(w => w.Title == model.Title);
            }

            if (model.TaskStatus.HasValue)
            {
                expressions.Add(w => w.TicketTaskStatus == model.TaskStatus.Value);
            }

            return expressions;
        }

    }
}

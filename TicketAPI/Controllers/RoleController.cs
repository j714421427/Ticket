using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ticket.Client;
using Ticket.Model;
using Ticket.Model.Enums;
using Ticket.Service;
using Ticket.ViewModel.Setting.Role;

namespace TicketAPI.Controllers
{
    [Route(WebApiControllerRoutePrefixes.Roles)]
    public class RoleController : ExceptionController
    {
        public readonly RoleService _RoleService;
        public RoleController(TicketContext context, RoleService RoleService)
        {
            _RoleService = RoleService;
        }

        [HttpGet("getall")]
        //[TypeFilter(typeof(ParameterBindingAttribute))]
        public RoleMain GetAll(RoleSearch search)
        {
            var result = new RoleMain();

            var query = BulidQuery(search);

            result.Items = _RoleService.GetAllQuery(query).Select(s => new RoleItem
            {
                Id = s.Id,
                Name = s.Name,
                Status = s.Status,
            }).ToList();

            return result;
        }

        [HttpGet]
        public RoleDetail Get(int id)
        {
            var entity = _RoleService.GetById(id);

            var result = new RoleDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                Status = entity.Status,
            };

            return result;
        }

        [HttpPost]
        public void Post([FromBody] RoleDetail model)
        {
            var item = new Role()
            {
                Name = model.Name,
                Status = model.Status,
            };

            _RoleService.Update(item);

            return;
        }

        [HttpPut]
        public void Put([FromBody] RoleDetail model)
        {
            var item = new Role()
            {
                Id = model.Id,
                Name = model.Name,
                Status = model.Status,
            };

            _RoleService.Update(item);

            return;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _RoleService.UpdateEntityStatus(id, EntityStatus.Deprecated);

            return;
        }



        private IEnumerable<Expression<Func<Role, bool>>> BulidQuery(RoleSearch model)
        {
            var expressions = new List<Expression<Func<Role, bool>>>();

            if (!string.IsNullOrEmpty(model.Name))
            {
                expressions.Add(w => w.Name == model.Name);
            }

            if (model.Status.HasValue)
            {
                expressions.Add(w => w.Status == model.Status.Value);
            }
            else
            {
                expressions.Add(w => new List<EntityStatus>() { EntityStatus.Enabled, EntityStatus.Disabled }.Contains(w.Status));
            }

            return expressions;
        }

    }
}

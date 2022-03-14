using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ticket.Client;
using Ticket.Model;
using Ticket.Model.Enums;
using Ticket.Service;
using Ticket.ViewModel.Setting.User;

namespace TicketAPI.Controllers
{
    [Route(WebApiControllerRoutePrefixes.Users)]
    public class UserController : ExceptionController
    {
        public readonly UserService _UserService;
        public UserController(TicketContext context, UserService UserService)
        {
            _UserService = UserService;
        }

        [HttpGet("getall")]
        [CustomAuthorization(PermissionCode.User_Access)]
        //[TypeFilter(typeof(ParameterBindingAttribute))]
        public UserMain GetAll(UserSearch search)
        {
            var result = new UserMain();

            var query = BulidQuery(search);

            result.Items = _UserService.GetAllQuery(query).Select(s => new UserItem
            {
                Id = s.Id,
                Name = s.Name,
                Account = s.Account,
                Status = s.Status,
            }).ToList();

            return result;
        }

        [HttpGet]
        public UserDetail Get(int id)
        {
            var entity = _UserService.GetById(id);

            var result = new UserDetail
            {
                Id = entity.Id,
                Name = entity.Name,
                Account = entity.Account,
                Password = entity.Password,
                Status = entity.Status,
            };

            return result;
        }

        [HttpPost]
        [CustomAuthorization(PermissionCode.User_Edit)]
        public void Post([FromBody] UserDetail model)
        {
            var item = new User()
            {
                Name = model.Name,
                Account = model.Account,
                Password = model.Password,
                Status = model.Status,
            };

            _UserService.Update(item);

            return;
        }

        [HttpPut]
        [CustomAuthorization(PermissionCode.User_Edit)]
        public void Put([FromBody] UserDetail model)
        {
            var item = new User()
            {
                Id = model.Id,
                Name = model.Name,
                Account = model.Account,
                Password = model.Password,
                Status = model.Status,
            };

            _UserService.Update(item);

            return;
        }

        [HttpDelete]
        [CustomAuthorization(PermissionCode.User_Delete)]
        public void Delete(int id)
        {
            _UserService.UpdateEntityStatus(id, EntityStatus.Deprecated);

            return;
        }



        private IEnumerable<Expression<Func<User, bool>>> BulidQuery(UserSearch model)
        {
            var expressions = new List<Expression<Func<User, bool>>>();

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

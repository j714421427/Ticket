using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticket.Client.Setting;
using Ticket.ViewModel.Setting.Role;

namespace Ticket.Controllers
{
    public class RoleController : ExceptionController
    {
        public readonly RoleClient _RoleClient;

        public RoleController(RoleClient roleClient)
        {
            _RoleClient = roleClient;
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

        public async Task<RoleMain> GetAll(RoleSearch query)
        {
            var result = await _RoleClient.Search(query);

            return result;
        }

        public async Task<RoleDetail> Get(int id)
        {
            var result = await _RoleClient.FindById(id);

            return result;
        }

        public async Task Save(RoleDetail model)
        {
            if (model.Id == 0)
            {
                var result = await _RoleClient.Create(model);
            }
            else
            {
                var result = await _RoleClient.Update(model);
            }

            return;
        }

        public async Task Delete(int id)
        {
            var result = await _RoleClient.Delete(id);

            return;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ticket.Client.Setting;
using Ticket.ViewModel.Setting.User;

namespace Ticket.Controllers
{
    public class UserController : ExceptionController
    {
        public readonly UserClient _UserClient;

        public UserController(UserClient userClient) {
            _UserClient = userClient;
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

        public async Task<UserMain> GetAll(UserSearch query)
        {
            var result = await _UserClient.Search(query);

            return result;
        }

        public async Task<UserDetail> Get(int id)
        {
            var result = await _UserClient.FindById(id);

            return result;
        }

        public async Task Save(UserDetail model)
        {
            if (model.Id == 0)
            {
                var result = await _UserClient.Create(model);
            }
            else
            {
                var result = await _UserClient.Update(model);
            }

            return ;
        }

        public async Task Delete(int id)
        {
            var result = await _UserClient.Delete(id);

            return;
        }
    }
}

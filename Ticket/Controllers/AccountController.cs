using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Client.Account;
using Ticket.Client.Setting;
using Ticket.ViewModel.Account;

namespace Ticket.Controllers
{
    public class AccountController : ExceptionController
    {
        public readonly AccountClient _AccountClient;

        public AccountController(AccountClient accountClient)
        {
            _AccountClient = accountClient;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string account = "",string password = "")
        {
            TempData["loginerror"] = null;

            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                TempData["loginerror"] = "account password cant be null";

                return LogIn();
            }

            var user = await _AccountClient.LogIn(new LoginMain(account, password));

            if (user == null)
            {
                TempData["loginerror"] = "logn fail";

                return LogIn();
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }
    }
}

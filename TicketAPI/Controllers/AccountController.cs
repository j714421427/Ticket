using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Ticket.Client;
using Ticket.Model;
using Ticket.Model.Enums;
using Ticket.Service;
using Ticket.ViewModel.Account;
using Ticket.ViewModel.Setting.User;
namespace TicketAPI.Controllers
{
    [Route(WebApiControllerRoutePrefixes.Account)]
    public class AccountController : ExceptionController
    {
        public readonly UserService _UserService;
        public readonly RoleService _RoleService;
        public AccountController(TicketContext context, UserService userService, RoleService roleService)
        {
            _UserService = userService;
            _RoleService = roleService;
        }
        public void Options()
        { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<UserItem> Post([FromBody]LoginMain model)
        {
            try
            {
                var user = _UserService.Login(model.Account, model.Password);

                user = _UserService.GetById(user.Id, "Role.RolePermissions.Permission");

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("Account", user.Id.ToString()));

                #region Permission Claims

                List<string> permimssionCodes = new List<string>();

                foreach (var permission in user.Role.RolePermissions.Select(s => s.Permission))
                {
                    claims.Add(new Claim("Permissions", permission.PermissionCode.ToString()));
                }

                #endregion

                // insert user info
                ClaimsIdentity userIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(userIdentity));

                return new UserItem() { Account = user.Account };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPut]
        public async void Put()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

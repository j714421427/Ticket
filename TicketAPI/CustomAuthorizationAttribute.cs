using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Ticket.Core;
using Ticket.Model.Enums;

namespace TicketAPI
{
    public class CustomAuthorization : TypeFilterAttribute
    {
        public CustomAuthorization(PermissionCode permission) : base(typeof(AuthorizeAction))
        {
            Arguments = new object[] {
            permission
        };
        }
    }

    public class AuthorizeAction : IAuthorizationFilter
    {
        public PermissionCode Permission { get; set; }
        public AuthorizeAction(PermissionCode code)
        {
            Permission = code;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var claims = context.HttpContext.User.Claims;

            if (!claims.Any())
            {
                throw new BusinessException("not login", BusinessErrorCode.LoginFail);
            }


            var permissions = claims.Where(c => c.Type == "Permissions");
            if (!permissions.Any(o => o.Value == Permission.ToString()))
            {
                throw new BusinessException("no permission", BusinessErrorCode.NoPermission);
            }
        }
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Client;
using Ticket.Core;

namespace TicketAPI
{
    public class ErrorHandling
    {
        private readonly RequestDelegate _next;

        public ErrorHandling(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {

                if (e is BusinessException bex)
                {
                    switch (bex.ErrorCode)
                    {
                        case BusinessErrorCode.LoginFail:
                        case BusinessErrorCode.NoPermission:
                            context.Response.StatusCode = 403;
                            var businessErrorResult = new BusinessErrorResult()
                            {
                                IsBusinessError = true,
                                Message = e.Message,
                                ErrorCode = bex.ErrorCode,
                            };
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(businessErrorResult));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}

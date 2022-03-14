using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ticket.ViewModel.Account;
using Ticket.ViewModel.Setting.User;

namespace Ticket.Client.Account
{
    public class AccountClient : WebApiClientBase
    {
        public AccountClient(string uri)
        {
            RoutePrefix = WebApiControllerRoutePrefixes.Account;

            client = client ?? new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        protected override HttpClient GetHttpClient()
        {
            return client;
        }

        public async Task<UserItem> LogIn(LoginMain model)
        {
            return await PostAsync<LoginMain, UserItem>("", model);
        }

        public async Task<bool> LogOut()
        {
            return await PutAsync("");
        }

    }
}

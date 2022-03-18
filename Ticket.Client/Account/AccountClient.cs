using System.Net.Http;
using System.Threading.Tasks;
using Ticket.ViewModel.Account;
using Ticket.ViewModel.Setting.User;

namespace Ticket.Client.Account
{
    public class AccountClient : WebApiClientBase
    {
        public AccountClient(HttpClient httpClient)
        {
            RoutePrefix = WebApiControllerRoutePrefixes.Account;

            client = httpClient;
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

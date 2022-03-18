using System.Net.Http;
using System.Threading.Tasks;
using Ticket.ViewModel.Setting.User;

namespace Ticket.Client.Setting
{
    public class UserClient : WebApiClientBase
    {

        public UserClient(HttpClient httpClient)
        {
            RoutePrefix = WebApiControllerRoutePrefixes.Users;

            client = httpClient;
        }

        protected override HttpClient GetHttpClient()
        {
            return client;
        }

        public async Task<UserMain> Search(UserSearch model)
        {
            uri = $"getall?{Core.Extensions.SerializeToQueryString(model)}";
            return await GetAsync<UserMain>(uri);
        }

        public async Task<UserDetail> FindById(int id)
        {
            uri = $"?id={id}";
            return await GetAsync<UserDetail>(uri);
        }

        public async Task<bool> Create(UserDetail model)
        {
            return await PostAsync("", model);
        }

        public async Task<bool> Update(UserDetail model)
        {
            return await PutAsync("", model);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync("", id);
        }
    }
}

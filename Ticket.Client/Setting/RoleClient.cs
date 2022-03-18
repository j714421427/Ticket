using System.Net.Http;
using System.Threading.Tasks;
using Ticket.ViewModel.Setting.Role;

namespace Ticket.Client.Setting
{
    public class RoleClient : WebApiClientBase
    {

        public RoleClient(HttpClient httpClient)
        {
            RoutePrefix = WebApiControllerRoutePrefixes.Roles;

            client = httpClient;
        }

        protected override HttpClient GetHttpClient()
        {
            return client;
        }

        public async Task<RoleMain> Search(RoleSearch model)
        {
            uri = $"getall?{Core.Extensions.SerializeToQueryString(model)}";
            return await GetAsync<RoleMain>(uri);
        }

        public async Task<RoleDetail> FindById(int id)
        {
            uri = $"?id={id}";
            return await GetAsync<RoleDetail>(uri);
        }

        public async Task<bool> Create(RoleDetail model)
        {
            return await PostAsync("", model);
        }

        public async Task<bool> Update(RoleDetail model)
        {
            return await PutAsync("", model);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync("", id);
        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Ticket.ViewModel.TickeyTask;

namespace Ticket.Client.TicketTask
{
    public class TicketTaskClient : WebApiClientBase
    {

        public TicketTaskClient(string uri)
        {
            RoutePrefix = WebApiControllerRoutePrefixes.TicketTasks;

            client = client ?? new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
        }

        protected override HttpClient GetHttpClient()
        {
            return client;
        }

        public async Task<TicketTaskMain> Search(TicketTaskSearch model)
        {
            uri = $"getall?{Core.Extensions.SerializeToQueryString(model)}";
            return await GetAsync<TicketTaskMain>(uri);
        }

        public async Task<TicketTaskDetail> FindById(int id)
        {
            uri = $"?id={id}";
            return await GetAsync<TicketTaskDetail>(uri);
        }

        public async Task<bool> Create(TicketTaskDetail model)
        {
            return await PostAsync("", model);
        }

        public async Task<bool> Update(TicketTaskDetail model)
        {
            return await PutAsync("", model);
        }

        public async Task<bool> Resolve(int id)
        {
            return await PatchAsync("", id);
        }

        public async Task<bool> Delete(int id)
        {
            return await DeleteAsync("", id);
        }
    }
}

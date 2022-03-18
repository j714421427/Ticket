using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Ticket.Client
{
    public abstract class WebApiClientBase
    {
        protected HttpClient client;
        protected string RoutePrefix = null;
        protected static string uri = string.Empty;

        protected abstract HttpClient GetHttpClient();

        protected async Task<TResult> GetAsync<TResult>(string uri)
        {
            var response = await GetHttpClient().GetAsync($"{RoutePrefix}/{uri}");

            return await response.ReadResult<TResult>();
        }

        protected async Task<bool> PostAsync<TModel>(string uri, TModel model)
        {
            var response = await GetHttpClient().PostAsync($"{RoutePrefix}/{uri}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        protected async Task<TResult> PostAsync<TModel, TResult>(string uri, TModel model)
        {
            var response = await GetHttpClient().PostAsync($"{RoutePrefix}/{uri}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            return await response.ReadResult<TResult>();
        }

        protected async Task<bool> PutAsync(string uri)
        {
            var response = await GetHttpClient().PutAsync($"{RoutePrefix}/{uri}", null);

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> PutAsync<TModel>(string uri, TModel model)
        {
            var response = await GetHttpClient().PutAsync($"{RoutePrefix}/{uri}", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> PatchAsync(string uri, int id)
        {
            var response = await GetHttpClient().PatchAsync($"{RoutePrefix}/{uri}?id={id}", null);

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> DeleteAsync(string uri, int id)
        {
            var response = await GetHttpClient().DeleteAsync($"{RoutePrefix}/{uri}?id={id}");

            return response.IsSuccessStatusCode;
        }

        protected async Task<bool> PatchApiResultAsync<TModel>(string uri, TModel model)
        {
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await GetHttpClient().SendAsync(new HttpRequestMessage(new HttpMethod("PATCH"), $"{RoutePrefix}/{uri}")
            {
                Content = byteContent
            });

            return response.IsSuccessStatusCode;
        }
    }

    public class BusinessErrorResult
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public bool IsBusinessError { get; set; }
    }
}

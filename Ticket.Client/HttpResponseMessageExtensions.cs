using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Ticket.Core;

namespace Ticket.Client
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task EnsureSuccessStatusCodeAsync(this HttpResponseMessage message)
        {
            if (!message.IsSuccessStatusCode)
            {
                throw new Exception(await message.Content?.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// For api actions that have no return value.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task ReadResult(this HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                var result = await message.Content?.ReadAsStringAsync();
            }
            else
            {
                throw await BuildException(message);
            }
        }

        /// <summary>
        /// For api actions that return a string value.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<string> ReadString(this HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                var result = await message.Content?.ReadAsStringAsync();

                return result;
            }
            else
            {
                throw await BuildException(message);
            }
        }

        /// <summary>
        /// For api actions that return a <typeparamref name="TResult"/> value. 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<TResult> ReadResult<TResult>(this HttpResponseMessage message)
        {
            if (message.IsSuccessStatusCode)
            {
                var resultString = await message.Content?.ReadAsStringAsync();
                TResult result = JsonConvert.DeserializeObject<TResult>(resultString);

                if (result == null)
                {
                    return default(TResult);
                }

                return result;
            }
            else
            {
                throw await BuildException(message);
            }
        }

        private static async Task<Exception> BuildException(HttpResponseMessage message)
        {
            var result = await message.Content?.ReadAsStringAsync();

            try
            {
                var businessErrorResult = JsonConvert.DeserializeObject<BusinessErrorResult>(result);

                return new BusinessException(businessErrorResult.Message, businessErrorResult.ErrorCode);
            }
            catch (Exception)
            {
                // do something
            }

            return new Exception(result);
        }
    }
}

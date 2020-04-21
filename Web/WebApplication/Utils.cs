using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication
{
    public static class Utils
    {
        public static Guid UserId = Guid.Empty;
        
        public static async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("ID", UserId.ToString());
            return await httpClient.PostAsync(url, byteContent);
        }
        
        public static async Task<HttpResponseMessage> GetAsync(string url)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("ID", UserId.ToString());
            return await httpClient.GetAsync(url);
        }
    }
}
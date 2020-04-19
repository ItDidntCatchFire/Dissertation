using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebApplication
{
    public static class Utils
    {
        public static string API_URL { get; set; }
        public static async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpClient = new HttpClient();
            return await httpClient.PostAsync($"{API_URL}{url}", byteContent);
        }
    }
}
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ScatterGatherDemo
{
    public static class HttpHelper
    {
        public async static Task<string> GetHtmlString(string url)
        {
            var client = new HttpClient {MaxResponseContentBufferSize = 1000000};
            return await client.GetStringAsync(url);
        }

        public async static Task<dynamic> GetJson(string url)
        {
            var data = await GetHtmlString(url);
            return JsonConvert.DeserializeObject<dynamic>(data);
        }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace VkSearchWPF.Vk
{
    public static class VkAccess
    {
        private static readonly HttpClient http = new HttpClient();

        public static string ApiUrl { get; } = "https://api.vk.com/method/";
        public static string ApiVersion { get; } = "5.80";

        public static async Task<VkApiResult> CallMethod(string method, Dictionary<string, string> methodParams = null)
        {
            methodParams = methodParams ?? new Dictionary<string, string>();
            methodParams["v"] = ApiVersion;
            var url = ApiUrl + method;
            var response = await http.PostAsync(url, new FormUrlEncodedContent(methodParams));
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<VkApiResult>(json);
        }
    }
}

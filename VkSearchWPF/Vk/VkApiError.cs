using Newtonsoft.Json;

namespace VkSearchWPF.Vk
{
    public class VkApiError
    {
        [JsonProperty("error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty("error_msg")]
        public string ErrorMsg { get; set; }
    }
}
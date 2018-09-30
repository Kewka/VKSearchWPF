using Newtonsoft.Json;

namespace VkSearchWPF.Config
{
    public class SearchConfig
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonProperty("lang")]
        public string Language { get; set; } = "en";
    }
}

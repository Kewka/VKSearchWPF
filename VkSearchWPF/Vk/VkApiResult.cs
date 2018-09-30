using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace VkSearchWPF.Vk
{
    public class VkApiResult
    {
        [JsonProperty("response")]
        public JToken Response { get; set; }
        [JsonProperty("error")]
        public VkApiError Error { get; set; }

        [JsonIgnore]
        public bool Success => Response != null;
    }
}

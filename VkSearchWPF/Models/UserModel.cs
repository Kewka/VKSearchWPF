using Newtonsoft.Json;

namespace VkSearchWPF.Models
{
    /// <summary>
    /// This class is intended to represent the user model of the VK API (<see href="https://vk.com/dev/objects/user" />).
    /// </summary>
    public class UserModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        [JsonProperty("photo_max")]
        public string PhotoUrl { get; set; }
        [JsonIgnore]
        public string FullName => FirstName + " " + LastName;
    }
}

using System.Text.Json.Serialization;

namespace User.Api
{
    public class UserDto
    {
        [JsonPropertyName("userName")]
        public string? UserName { get; set; }
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace User.Api.Core
{
    public class UserDataResponse
    {
        [JsonPropertyName("results")]
        public List<UserResult> Results { get; set; }
    }

    public class UserResult
    {
        [JsonPropertyName("name")]
        public UserName Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("login")]
        public LoginInfo Login { get; set; }

        [JsonPropertyName("dob")]
        public DateOfBirth Dob { get; set; }
    }


    public class UserName
    {
        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }
    }

    public class LoginInfo
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }

    public class DateOfBirth
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }

    // Clase para procesar y extraer los datos
    public class UserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RawPassword { get; set; }
        public DateTime BirthDate { get; set; }

        public static User FromApiResponse(UserDataResponse apiResponse)
        {
            if (apiResponse?.Results == null || apiResponse.Results.Count == 0)
                return null;

            var firstResult = apiResponse.Results[0];

            return new User
            {
                FirstName = firstResult.Name?.First,
                LastName = firstResult.Name?.Last,
                Email = firstResult.Email,
                UserName = firstResult.Login?.Username,
                Password = firstResult.Login?.Password,
                RawPassword = firstResult.Login?.Password,
                BirthDate = firstResult.Dob?.Date ?? DateTime.MinValue
            };
        }
    }
}

using System.Net.Http;
using System.Text.Json;

namespace User.Api.Core
{
    public class SaveUserBusiness
    {
        private readonly IHttpClientFactory _httpClient;
        public SaveUserBusiness(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<ServiceState> Process()
        {
            try
            {
                var client = _httpClient.CreateClient("RandomUser");
                var response = await client.GetAsync("api/");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var dataUser = JsonSerializer.Deserialize<UserDataResponse>(json);

                    var user = UserInfo.FromApiResponse(dataUser);

                    user.Password = PasswordHash.Generate(user.UserName, user.Password);

                    Datebase.Instance.ExecuteNonQuery(user);
                }

                return ServiceState.Accepted;

            }
            catch (Exception ex)
            {
                return ServiceState.Rejected;
                throw new Exception("Error al procesar la solicitud", ex);
            }
        }
    }
}

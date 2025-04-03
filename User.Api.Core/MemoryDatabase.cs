using System.Text.Json;

namespace User.Api.Core
{
    public class MemoryDatabase : Datebase
    {
        private static readonly List<User> UserList  = new List<User>();
        
        public static void Initialize()
        {
            if (Instance != null)
            {
                throw new Exception("La base de datos ya ha sido inicializada");
            }
            Instance = new MemoryDatabase();
        }
        public override void ExecuteNonQuery(User user)
        {
            if (user is null)
                throw new ArgumentNullException("el usuario no puede ser nulo");

            if(UserList.Any(u => u.UserName == user.UserName))
                throw new Exception("El usuario ya existe");

            UserList.Add(user);
        }

        public override string GetJson(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return JsonSerializer.Serialize(UserList, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            var user = UserList.FirstOrDefault(u => u.UserName == userName);
            return JsonSerializer.Serialize(user, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}

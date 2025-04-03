using System.Security.Cryptography;
using System.Text;

namespace User.Api.Core
{
    public static class PasswordHash
    {
        public static string Generate(string userName, string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("La contraseña no puede ser nula o vacía");
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("El usuario no puede ser nula o vacía");
            }

            string combinedString = $"{userName.ToLower()}|{password}";
            using (SHA256 sha256 = SHA256.Create())
            {

                byte[] bytes = Encoding.UTF8.GetBytes(combinedString);

                byte[] hashBytes = sha256.ComputeHash(bytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public static ServiceState Validate(string userName, string password, string hash)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("La contraseña no puede ser nula o vacía");
            }
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("El usuario no puede ser nula o vacía");
            }
            if (string.IsNullOrEmpty(hash))
            {
                throw new ArgumentNullException("El hash no puede ser nulo o vacío");
            }

            try
            {
                string generatedHash = Generate(userName, password);

                return SecureCompare(generatedHash, hash)
                    ? ServiceState.Accepted
                    : ServiceState.Rejected;
            }
            catch (Exception ex)
            {
                return ServiceState.Rejected;
                throw new Exception("Error al validar la contraseña", ex);

            }
        }
        private static bool SecureCompare(string a, string b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }

            return result == 0;
        }
    }
}
    

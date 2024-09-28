using System.Security.Cryptography;
using System.Text;
using API.Contract.IService;

namespace API.Service
{
    public class PasswordHasherService : IPasswordHasherService
    {
        private string GenerateSalt(int size)
        {
            var salt = new byte[size];
            using (var rg = new RNGCryptoServiceProvider())
            {
                rg.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public (string hashedPassword, string salt) HashPassword(string password)
        {
            var salt = GenerateSalt(16); 
            var hashedPassword = HashWithSalt(password, salt);
            return (hashedPassword, salt);
        }

        private string HashWithSalt(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Convert.FromBase64String(salt);
            var combinedBytes = new byte[passwordBytes.Length + saltBytes.Length];

            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, passwordBytes.Length, saltBytes.Length);

            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string salt, string hashedPassword)
        {
            var inputPassword = HashWithSalt(password, salt);
            return inputPassword.Equals(hashedPassword);
        }
    }
}

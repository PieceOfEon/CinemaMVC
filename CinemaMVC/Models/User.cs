using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace CinemaMVC.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Logins { get; set; }

        [Required]
        public string Passwords { get; set; }

        public void SetPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                Passwords = Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                string hashedInput = Convert.ToBase64String(hashBytes);
                return Passwords == hashedInput;
            }
        }

    }
}

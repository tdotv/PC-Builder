using System.Security.Cryptography;
using System.Text;

namespace PC_Designer.Server
{
    public class Utility
    {
        public static string Encrypt(string password)
        {
            string salt = "S0m3R@nd0mSalt";
            byte[] bytes = MD5.HashData(Encoding.UTF32.GetBytes(salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
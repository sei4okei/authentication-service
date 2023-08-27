using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationService.Helpers
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "issuer"; // издатель токена
        public const string AUDIENCE = "audience"; // потребитель токена
        const string KEY = "metanit_helps_af";   // ключ для шифрации
        public const int LIFETIME = 1; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}

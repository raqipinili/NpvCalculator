using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Security.Core.Helpers
{
    public class AuthHelper
    {
        public static SigningCredentials GetSigningCredentials(string secretKey)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}

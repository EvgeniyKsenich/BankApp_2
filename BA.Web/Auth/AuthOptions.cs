using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BA.Web.Auth
{
    public class AuthOptions 
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(string KEY)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}

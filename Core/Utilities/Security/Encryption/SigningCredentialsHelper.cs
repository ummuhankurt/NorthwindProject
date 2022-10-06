using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        // Sisteme girebilmek için bir anahtara ihtiyaç vardı ya, credentials dediğimiz bizim anahtarımız.
        // Kullanıcı adı ve şifre Credentialstır.Bir sisteme girebilmek için elinizde olanlardır.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}

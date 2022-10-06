using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    // Bu bizim için bir araç olduğundan, çıplak kalabilir.
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password , out byte[] passwordHash, out byte [] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // Bu algoritmanın değişmeyen anahtarı.Bizim bildiğimiz yapı.Her kullanıcı için farklı bir key oluşturur.
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        // sisteme sonradan girmeye çalışan kişinin passwordünün kontrolü.
        public static bool VerifyPasswordHash(string password,byte[] passwordHash,byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}

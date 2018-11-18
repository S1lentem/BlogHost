using BlogHostCore.Interfaces;

using System;
using System.Security.Cryptography;

namespace SupportLibrary.Protection
{
    public class PasswordHashingManager : IHashable
    {
        public string Password { get; set; }

        public PasswordHashingManager(string password) => this.Password = password;

        public PasswordHashingManager() : this(null) { }

        public string GetHash()
        {
            if (Password == null) return null;
            byte[] salt, buffer2;
            
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(Password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public bool VerifyPassword(string hashToCheck)
        {
            if (Password == null) return false;

            byte[] buffer4;
            
            byte[] src = Convert.FromBase64String(hashToCheck);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(Password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        private static bool ByteArraysEqual(byte[] firstArray, byte[] secondArray)
        {
            if (firstArray == secondArray) return true;
            if (firstArray == null || secondArray == null || firstArray.Length != secondArray.Length) return false;
            for (int i = 0; i < firstArray.Length; i++)
            {
                if (firstArray[i] != secondArray[i]) return false;
            }
            return true;
        }
    }
}

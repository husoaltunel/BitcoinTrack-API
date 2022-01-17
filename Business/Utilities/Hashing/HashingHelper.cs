using Business.Utilities.Hashing.Abstract;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Business.Utilities.Hashing
{
    public class HashingHelper : IHashingHelper
    {
        private byte[] _passwordHash;
        private byte[] _passwordSalt;
        public void GeneratePasswordHashAndSalt(string password)
        {
            using (var hmac = new HMACSHA512())
            {
                _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                _passwordSalt = hmac.Key;
            }
        }
        public byte[] GetPasswordHash()
        {
            return _passwordHash;
        }
        public byte[] GetPasswordSalt()
        {
            return _passwordSalt;
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = passwordSalt;
                _passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return CheckPasswordHashEqual(passwordHash);
            }
        }
        private bool CheckPasswordHashEqual(byte[] passwordHash)
        {
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != _passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}

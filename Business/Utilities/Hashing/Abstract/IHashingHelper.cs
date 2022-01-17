using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Hashing.Abstract
{
    public interface IHashingHelper
    {
        public void GeneratePasswordHashAndSalt(string password);
        public byte[] GetPasswordHash();

        public byte[] GetPasswordSalt();
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}

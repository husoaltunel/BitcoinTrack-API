
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Security.Helpers
{
    public class SecurityKeyHelper
    {
        public static SymmetricSecurityKey CreateSymmetricSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}

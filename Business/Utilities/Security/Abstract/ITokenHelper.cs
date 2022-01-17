using Entities;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Security.Abstract
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}

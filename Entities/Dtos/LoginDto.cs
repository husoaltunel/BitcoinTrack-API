using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class LoginDto : IDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

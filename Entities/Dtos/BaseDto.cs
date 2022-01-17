using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BaseDto : IDto
    {
        public int Id { get; set; }
    }
}

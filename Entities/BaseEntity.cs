﻿using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}

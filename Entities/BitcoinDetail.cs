using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class BitcoinDetail :BaseEntity, IEntity
    {
        public decimal Price { get;set;}
        public DateTime Date { get;set;}
    }
}

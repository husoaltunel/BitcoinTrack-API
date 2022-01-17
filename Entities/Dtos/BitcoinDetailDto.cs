using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BitcoinDetailDto
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}

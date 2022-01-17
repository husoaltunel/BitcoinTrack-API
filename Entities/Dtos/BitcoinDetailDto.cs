using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class BitcoinDetailDto : BaseDto
    {
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}

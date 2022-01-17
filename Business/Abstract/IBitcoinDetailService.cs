using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBitcoinDetailService
    {
        public Task<IEnumerable<BitcoinDetailDto>> GetAllAsync();
        public void InsertAsync(BitcoinDetailDto bitcoinDetail);
    }
}

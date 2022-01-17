using Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBitcoinDetailRepository
    {
        public Task<IEnumerable<BitcoinDetail>> GetAllAsync();
        public Task<int> InsertAsync(BitcoinDetail bitcoinDetail);
    }
}

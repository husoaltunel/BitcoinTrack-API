using AutoMapper;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.BitcoinDetail
{
    public class BitcoinDetailService : IBitcoinDetailService
    {
        private readonly IBitcoinDetailRepository _bitcoinDetailRepository;
        private readonly IMapper _mapper;
        public BitcoinDetailService(IBitcoinDetailRepository bitcoinDetailRepository, IMapper mapper)
        {
            _bitcoinDetailRepository = bitcoinDetailRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BitcoinDetailDto>> GetAllAsync()
        {
            var details = await _bitcoinDetailRepository.GetAllAsync();
            var mappedDetails = details.Select(detail => _mapper.Map<BitcoinDetailDto>(detail));

            return mappedDetails;
        }

        public void InsertAsync(BitcoinDetailDto bitcoinDetail)
        {
            var mappedBitcoinDetail = _mapper.Map<Entities.BitcoinDetail>(bitcoinDetail);
            _bitcoinDetailRepository.InsertAsync(mappedBitcoinDetail);
        }
    }
}

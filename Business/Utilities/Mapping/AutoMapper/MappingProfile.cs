using AutoMapper;
using Entities;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<BitcoinDetail, BitcoinDetailDto>().ForMember(
                dest => dest.Date,
        opt => opt.MapFrom(src => src.Date.AddTicks(-(src.Date.Ticks % TimeSpan.TicksPerSecond)))
                ).ReverseMap();

        }
    }
}

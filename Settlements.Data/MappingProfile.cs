using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Settlements.Data
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Settlement, SettlementDTOPost>().ForMember(d => d.Id, o => o.Ignore()).ReverseMap();
            CreateMap<Settlement, SettlementDTOGet>();
            CreateMap<Settlement, SettlementDTOPut>().ReverseMap();
            CreateMap<CountryDTO, Country>().ForMember(d => d.Id, o => o.MapFrom(src => src.Id))
                .ForMember(d => d.CountryName, o => o.MapFrom(src => src.CountryName))
                .ForMember(d => d.Settlements, o => o.Ignore());
            //CreateMap<PagedList<Settlement>, PagedList<SettlementDTOGet>>();
        }
    }
}

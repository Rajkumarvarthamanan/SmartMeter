using AutoMapper;
using SmartMeterAPI.Data;
using SmartMeterAPI.Dto;

namespace SmartMeterAPI.Helpers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<ReadingDto, MeterReading>();
            CreateMap<MeterReading, ReadingDto>();
        }
        
    }
}

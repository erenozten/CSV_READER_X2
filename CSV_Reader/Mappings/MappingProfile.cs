using AutoMapper;
using CSV_Reader.Models;

namespace CSV_Reader.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Hotel, HotelViewModel>();
            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelsGrouped, HotelDto>();
        }
    }
}

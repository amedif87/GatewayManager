using GatewayManager.Domain.DTOs;
using GatewayManager.Domain.Entities;

namespace GatewayManager.Domain.Profile
{
    using AutoMapper;
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Gateway, GatewayDTO>().ReverseMap();
            CreateMap<PeripheralDevice, PeripheralDeviceDTO>()
                .ForMember(destination => destination.GatewayName, opt => opt.MapFrom(source => source.Gateway.Name))
                .ForMember(destination => destination.Status, opt => opt.MapFrom(source => source.Status.ToString()));
            CreateMap<PeripheralDeviceDTO, PeripheralDevice>();
           
        }
    }
}

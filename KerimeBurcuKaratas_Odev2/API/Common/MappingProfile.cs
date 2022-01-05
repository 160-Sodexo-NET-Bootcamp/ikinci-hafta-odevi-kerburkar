using API.Dtos;
using API.Model;
using AutoMapper;
using Data.DataModel;

namespace API.Common
{
    //Gösterilmemesi gereken alanların kullanıcıya dönmemesi için modeller dto'lara dönüştürüldü.
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Vehicle, VehicleDto>().ReverseMap();
            CreateMap<Container, ContainerDto>().ReverseMap();
            CreateMap<Container, ContainerDistanceModel>().ReverseMap();
            CreateMap<ContainerDto, ContainerDistanceModel>().ReverseMap();
        }
    }
}

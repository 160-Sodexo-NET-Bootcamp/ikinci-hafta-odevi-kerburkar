using API.Dtos;
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

        }
    }
}

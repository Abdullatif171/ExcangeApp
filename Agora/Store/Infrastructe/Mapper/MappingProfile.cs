using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Store.Infrastructe.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForInsertin, Product>();
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap();
        }
        
    }
}
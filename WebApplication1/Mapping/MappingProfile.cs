using AutoMapper;
using WebApplication1.DTOs;
using WebApplication1.Entities;

namespace WebApplication1.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                   .ReverseMap();
            
        }
    }

}

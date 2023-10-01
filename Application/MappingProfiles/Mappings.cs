using Application.Models.Images;
using Application.Models.Properties;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NewProperty, Property>();
            CreateMap<UpdateProperty, Property>();
            CreateMap<Property, PropertyDto>();
            CreateMap<NewImage, Image>();
            CreateMap<UpdateImage, Image>();
            CreateMap<Image, ImageDto>();

        }
    }
}

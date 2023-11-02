using AutoMapper;
using System.Linq;
using Web2.API.Data.Models;
using Web2.API.DTO;

namespace Web2.API
{
    public class Web2ApiMappingProfile : Profile
    {
        public Web2ApiMappingProfile()
        {
            CreateMap<Evenement, EvenementDTO>().ForMember(dest => dest.CategoryIDs, opt => opt.MapFrom(src => src.Categories.Select(c => c.ID).ToList()));
            CreateMap<Ville, VilleDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Participation, ParticipationDTO>();
        }
    }
}

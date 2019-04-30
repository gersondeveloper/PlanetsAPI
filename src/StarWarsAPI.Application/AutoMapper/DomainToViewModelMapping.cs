using AutoMapper;
using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;

namespace StarWarsAPI.Application.AutoMapper
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<Planet, PlanetViewModel>().ReverseMap();
        }
    }
}

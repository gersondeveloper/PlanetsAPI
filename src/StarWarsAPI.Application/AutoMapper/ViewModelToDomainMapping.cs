using AutoMapper;
using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWarsAPI.Application.AutoMapper
{
    public class ViewModelToDomainMapping : Profile
    {

        public ViewModelToDomainMapping()
        {
            CreateMap<PlanetViewModel, Planet>();
        }
    }
}

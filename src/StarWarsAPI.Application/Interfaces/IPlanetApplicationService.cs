using MongoDB.Bson;
using StarWarsAPI.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI.Application.Interfaces
{
    public interface IPlanetApplicationService
    {
        Task<IEnumerable<PlanetViewModel>> GetAllPlanets();
        Task<PlanetViewModel> GetPlanetById(int id);
        Task CreatePlanet(PlanetViewModel planet);
        Task<bool> RemovePlanet(int id);
    }
}

using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWarsAPI.Application.Interfaces
{
    public interface IPlanetApplicationService
    {
        Task<IEnumerable<PlanetViewModel>> GetAllPlanets();
        Task<PlanetViewModel> GetPlanetById(int id);
        Task<PlanetViewModel> GetPlanetByName(string name);
        Task<bool> CreatePlanet(Planet planet);
        Task<bool> RemovePlanet(int id);
    }
}

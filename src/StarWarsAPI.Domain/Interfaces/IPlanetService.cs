using MongoDB.Bson;
using StarWarsAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWarsAPI.Domain.Interfaces
{
    public interface IPlanetService
    {
        Task<IEnumerable<Planet>> GetAllPlanets();
        Task<Planet> GetPlanetById(int id);
        Task<Planet> GetPlanetByName(string name);
        Task<bool> CreatePlanet(Planet planet);
        Task<bool> RemovePlanet(int id);
    }
}

using MongoDB.Bson;
using StarWarsAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarWarsAPI.Domain.Interfaces
{
    public interface IPlanetRepository
    {
        Task<IEnumerable<Planet>> GetAllPlanets();
        Task<Planet> GetPlanet(string name);
        Task<Planet> GetPlanetById(ObjectId id);
        Task CreatePlanet(Planet planet);
        Task<bool> RemovePlanet(string name);
    }
}

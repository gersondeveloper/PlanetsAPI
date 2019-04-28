using MongoDB.Driver;
using StarWarsAPI.Domain.Entities;

namespace StarWarsAPI.Infra.Interfaces
{
    public interface IPlanetContext
    {
        IMongoCollection<Planet> Planets { get; }
    }
}

using MongoDB.Bson;
using MongoDB.Driver;
using StartWarsAPI.Infra.Interfaces;
using StarWarsAPI.Domain.Entities;
using StarWarsAPI.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartWarsAPI.Infra.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly IPlanetContext _context;

        public PlanetRepository(IPlanetContext context)
        {
            _context = context;
        }

        public async Task CreatePlanet(Planet planet)
        {
            await _context.Planets.InsertOneAsync(planet);
        }

        public async Task<IEnumerable<Planet>> GetAllPlanets()
        {
            return await _context.Planets.Find(planet => true).ToListAsync();
        }

        public async Task<Planet> GetPlanet(string name)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Name, name);
            return await _context.Planets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Planet> GetPlanetById(ObjectId id)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Id, id);
            return await _context.Planets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> RemovePlanet(string name)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Name, name);
            DeleteResult deleteResult = await _context.Planets.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}

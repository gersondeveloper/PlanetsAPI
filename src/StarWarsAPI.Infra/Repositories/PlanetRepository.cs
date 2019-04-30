using MongoDB.Driver;
using StarWarsAPI.Infra.Interfaces;
using StarWarsAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarWarsAPI.Application.Interfaces;
using StarWarsAPI.Domain.Interfaces;

namespace StarWarsAPI.Infra.Repositories
{
    public class PlanetRepository : IPlanetRepository
    {
        private readonly IPlanetContext _context;

        public PlanetRepository(IPlanetContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePlanet(Planet planet)
        {
            var result = await _context.Planets.Find(p => p.Id == planet.Id).FirstOrDefaultAsync();
            if (result == null)
            {
                await _context.Planets.InsertOneAsync(planet);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<Planet>> GetAllPlanets()
        {
            return await _context.Planets.Find(planet => true).ToListAsync();
        }

        public async Task<Planet> GetPlanetById(int id)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Id, id);
            return await _context.Planets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Planet> GetPlanetByName(string name)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Name, name);
            return await _context.Planets.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> RemovePlanet(int id)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _context.Planets.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

    }
}

using MongoDB.Driver;
using StarWarsAPI.Domain.Entities;
using StarWarsAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsAPI.Domain.Services
{
    public class PlanetService : IPlanetService
    {
        private readonly IPlanetRepository _planetRepository;

        public PlanetService(IPlanetRepository planetRepository)
        {
            _planetRepository = planetRepository;
        }

        public async Task CreatePlanet(Planet planet)
        {
            await _planetRepository.CreatePlanet(planet);
        }

        public async Task<IEnumerable<Planet>> GetAllPlanets()
        {
            return await _planetRepository.GetAllPlanets();
        }

        public async Task<Planet> GetPlanetById(int id)
        {
            return await _planetRepository.GetPlanetById(id);
        }

        public async Task<bool> RemovePlanet(int id)
        {
            return await _planetRepository.RemovePlanet(id);
        }
    }
}

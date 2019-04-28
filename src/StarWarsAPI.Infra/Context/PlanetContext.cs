using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StarWarsAPI.Infra.Interfaces;
using StarWarsAPI.Domain.Entities;
using System;

namespace StarWarsAPI.Infra.Context
{
    public class PlanetContext : IPlanetContext
    {
        private readonly IMongoDatabase _db;

        public PlanetContext(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("StarWarsConnectionString"));
            _db = client.GetDatabase("StarWarsDB");
            
        }

        public IMongoCollection<Planet> Planets => _db.GetCollection<Planet>("Planets");
    }
}

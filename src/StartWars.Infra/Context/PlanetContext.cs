using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StartWarsAPI.Infra.Interfaces;
using StarWarsAPI.Domain.Entities;
using System;

namespace StartWarsAPI.Infra.Context
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

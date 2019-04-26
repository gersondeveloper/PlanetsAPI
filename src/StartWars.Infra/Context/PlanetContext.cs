using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;
using StartWarsAPI.Infra.Interfaces;
using StarWarsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartWarsAPI.Infra.Context
{
    public class PlanetContext : IPlanetContext
    {
        private readonly IMongoDatabase _db;

        public PlanetContext(IConfiguration config)
        {

        }

        public IMongoCollection<Planet> Planets => throw new NotImplementedException();
    }
}

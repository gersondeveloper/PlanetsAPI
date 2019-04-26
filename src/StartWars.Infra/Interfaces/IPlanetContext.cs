using MongoDB.Driver;
using StarWarsAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StartWarsAPI.Infra.Interfaces
{
    public interface IPlanetContext
    {
        IMongoCollection<Planet> Planets { get; }
    }
}

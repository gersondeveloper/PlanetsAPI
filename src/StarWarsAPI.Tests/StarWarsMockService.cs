using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;
using System.Collections.Generic;

namespace StarWarsAPI.Tests
{
    public static class StarWarsMockService
    {

        public static IEnumerable<PlanetViewModel> GetAllPlanets()
        {
            IEnumerable<PlanetViewModel> planets = new List<PlanetViewModel>()
            {
                new PlanetViewModel(){ Id = 1, Name = "Teste1", Climate = "Climate1", Terrain = "Terrain1", AppearanceInMovies = 1 },
                new PlanetViewModel(){ Id = 2, Name = "Teste2", Climate = "Climate2", Terrain = "Terrain2", AppearanceInMovies = 2 },
                new PlanetViewModel(){ Id = 3, Name = "Teste3", Climate = "Climate3", Terrain = "Terrain3", AppearanceInMovies = 3 },
                new PlanetViewModel(){ Id = 4, Name = "Teste4", Climate = "Climate4", Terrain = "Terrain4", AppearanceInMovies = 4 }
            };

            return planets;
        }

        public static PlanetViewModel GetPlanetOK()
        {
            PlanetViewModel planetOk = new PlanetViewModel()
            {
                Id = 10,
                Name = "Planet Test",
                Climate = "Climate Test",
                Terrain = "Terrain Test",
                AppearanceInMovies = 4
            };

            return planetOk;
        }

        public static PlanetViewModel GetPlanetFail()
        {
            PlanetViewModel planetFail = new PlanetViewModel()
            {
                Name = "Planet Test With much more than 30 characteres",
                Climate = "Climate Test With much more than 30 characteres",
                Terrain = "T",
                AppearanceInMovies = 4
            };
            return planetFail;
        }

        public static Planet GetPlanetDomain()
        {
            Planet planetDomain = new Planet()
            { 
                Id = 10,
                Name = "Planet Domain",
                Climate = "Climate Domain",
                Terrain = "Terrain Domain",
                AppearanceInMovies = 4
            };
            return planetDomain;
        }

    }
}
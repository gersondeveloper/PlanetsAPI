using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using StarWarsAPI.Application.Interfaces;
using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;
using StarWarsAPI.Domain.Interfaces;

namespace StarWarsAPI.Domain.Services
{
    public class PlanetApplicationService : IPlanetApplicationService
    {
        private readonly IPlanetService _service;

        public PlanetApplicationService(IPlanetService service) : base()
        {
            _service = service;
        }

        public async Task CreatePlanet(PlanetViewModel planetViewModel)
        {
            var _appearanceInMovies = await GetAppearanceInMovies(planetViewModel);
            var planet = Mapper.Map<PlanetViewModel, Planet>(planetViewModel);
        }

        public async Task<IEnumerable<PlanetViewModel>> GetAllPlanets()
        {
            return await Mapper.Map<Task<IEnumerable<Planet>>, Task<IEnumerable<PlanetViewModel>>>(_service.GetAllPlanets());
        }

        public Task<bool> RemovePlanet(int id)
        {
            throw new System.NotImplementedException();
        }

        Task<PlanetViewModel> IPlanetApplicationService.GetPlanetById(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<PlanetViewModel> GetAppearanceInMovies(PlanetViewModel planetViewModel)
        {

            using (var client = new HttpClient { BaseAddress = new Uri("https://swapi.co") })
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    planetViewModel = await GetPlanetAsync(planetViewModel, client);
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return planetViewModel;
            }
        }

        private async Task<PlanetViewModel> GetPlanetAsync(PlanetViewModel planetViewModel, HttpClient client)
        {
            var response = await client.GetAsync($"api/planets/{planetViewModel.Id}/");
            if (response.IsSuccessStatusCode)
            {
                var planet = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(planet);
                var movieQuantity = json.GetValue("films").ToList();
                planetViewModel.AppearanceInMovies = movieQuantity.Count;
            }

            return planetViewModel;
        }

    }
}

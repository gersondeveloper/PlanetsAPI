using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json.Linq;
using StarWarsAPI.Application.Interfaces;
using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;
using StarWarsAPI.Domain.Services;

namespace StarWarsAPI.Application.Services
{
    public class PlanetApplicationService : IPlanetApplicationService
    {
        private readonly PlanetService _service;
        private readonly IMapper _mapper;

        public PlanetApplicationService(PlanetService service, IMapper mapper) : base()
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<bool> CreatePlanet(Planet planet)
        {
            var planetViewModel = _mapper.Map<Planet, PlanetViewModel>(planet);
            var _appearanceInMovies = await GetAppearanceInMovies(planetViewModel);
            var _planet = _mapper.Map<PlanetViewModel, Planet>(planetViewModel);
            return await _service.CreatePlanet(_planet);
            
        }

        public async Task<IEnumerable<PlanetViewModel>> GetAllPlanets()
        {
            var planetsWithAppearance = new List<PlanetViewModel>();
            var result = await _service.GetAllPlanets();
            var resultMapped = _mapper.Map<List<PlanetViewModel>>(result);

            foreach (var item in resultMapped)
            {
                var newPlanetWithAppearance = await GetAppearanceInMovies(item);
                planetsWithAppearance.Add(newPlanetWithAppearance);
            }

            return planetsWithAppearance;
        }

        public async Task<bool> RemovePlanet(int id)
        {
            return await _service.RemovePlanet(id);
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

        public async Task<PlanetViewModel> GetPlanetByName(string name)
        {
            var result = await _service.GetPlanetByName(name);
            var resultMapped = _mapper.Map<PlanetViewModel>(result);
            if (resultMapped != null)
            {
                return await GetAppearanceInMovies(resultMapped);
            }
            else
            {
                return null;
            }
        }

        public async Task<PlanetViewModel> GetPlanetById(int id)
        {
            var result = await _service.GetPlanetById(id);
            var resultMapped = _mapper.Map<PlanetViewModel>(result);
            if (resultMapped != null)
            {
                return await GetAppearanceInMovies(resultMapped);
            }
            else
            {
                return null;
            }
        }
    }
}

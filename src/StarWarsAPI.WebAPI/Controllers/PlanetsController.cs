using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Interfaces;

namespace StarWarsAPI.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PlanetsController : Controller
    {
        private readonly IPlanetService _planetService;
        private readonly IMapper _mapper;

        public PlanetsController(IPlanetService planetService, IMapper mapper)
        {
            _planetService = planetService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(PlanetViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _planetService.GetAllPlanets();
            var resultMapped = _mapper.Map<List<PlanetViewModel>>(result);
            return new ObjectResult(resultMapped);
        }
    }
}
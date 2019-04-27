using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Domain.Interfaces;

namespace StarWarsAPI.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PlanetsController : Controller
    {
        private readonly IPlanetService _planetService;

        public PlanetsController(IPlanetService planetService)
        {
            _planetService = planetService;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_planetService.GetAllPlanets());
        }
    }
}
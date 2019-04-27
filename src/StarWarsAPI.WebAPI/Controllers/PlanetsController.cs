﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWarsAPI.Application.Interfaces;
using StarWarsAPI.Application.ViewModels;
using StarWarsAPI.Domain.Entities;

namespace StarWarsAPI.WebAPI.Controllers
{
    [Produces("application/json")]
    public class PlanetsController : Controller
    {
        private readonly IPlanetApplicationService _planetService;
        private readonly IMapper _mapper;

        public PlanetsController(IPlanetApplicationService planetService, IMapper mapper)
        {
            _planetService = planetService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(PlanetViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _planetService.GetAllPlanets();
            var resultMapped = _mapper.Map<List<PlanetViewModel>>(result);
            return new ObjectResult(resultMapped);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        [ProducesResponseType(typeof(PlanetViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PlanetViewModel), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _planetService.GetPlanetById(id);
            var resultMapped = _mapper.Map<PlanetViewModel>(result);
            if (resultMapped != null)
            {
                return new OkObjectResult(resultMapped);
            }
            else
            {
                return new NotFoundObjectResult("Planet not found!");
            }
        }

        [HttpPost]
        [Route("api/[controller]")]
        [ProducesResponseType(typeof(PlanetViewModel), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(PlanetViewModel), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Post([FromBody] Planet planet)
        {
            List<string> _errorMessages = new List<string>();

            var _validator = new PlanetValidator();
            var _result = _validator.Validate(planet);

            if (!ModelState.IsValid)
            {
                foreach (var error in _result.Errors)
                {
                    _errorMessages.Add(error.ErrorMessage);
                }
                return new BadRequestObjectResult(_errorMessages);
            }
            else
            {
                var result = await _planetService.CreatePlanet(planet);
                if (result)
                {
                    return new OkObjectResult("Planet sucessfull created!");
                }
                else
                {
                    return new BadRequestObjectResult("Error on creating planet!");
                }
            }
        }

    }
}
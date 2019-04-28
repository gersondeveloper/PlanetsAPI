using StarWarsAPI.Domain.Entities;
using Xunit;
using FluentValidation.TestHelper;
using AutoMapper;
using StarWarsAPI.Application.AutoMapper;
using StarWarsAPI.Application.ViewModels;
using Moq;
using StarWarsAPI.Application.Interfaces;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace StarWarsAPI.Tests
{
    public class StarWarsUnitTests
    {
        private PlanetValidator _planetValidator;
        private IMapper _mapper;

        private Mock<IPlanetApplicationService> _mock;

        public StarWarsUnitTests()
        {
            //Initialize PlanetValidator
            _planetValidator = new PlanetValidator();

            //Initialize AutoMapper
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<DomainToViewModelMapping>();
            });

            _mapper  = new Mapper(config);

            //Initialize Mock
            _mock = new Mock<IPlanetApplicationService>();
            
           
        }

        #region Model Validator

        //Should have no errors in model
        [Fact]
        public void ShouldReturnNoValidationErrorForId()
        {
            var planet = StarWarsMockService.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Id, resultMapped.Id);
        }

        [Fact]
        public void ShouldReturnNoValidationErrorForName()
        {
            var planet = StarWarsMockService.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Name, resultMapped.Name);
        }

        [Fact]
        public void ShouldReturnNoValidationErrorForClimate()
        {
            var planet = StarWarsMockService.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Climate, resultMapped.Climate);
        }

        [Fact]
        public void ShouldReturnNoValidationErrorForTerrain()
        {
            var planet = StarWarsMockService.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Terrain, resultMapped.Terrain);
        }

        //Should present errors in wrong model
        [Fact]
        public void ShoulReturnErrorForNoId()
        {
            var planet = StarWarsMockService.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Id, resultMapped.Id);
        }

        [Fact]
        public void ShouldReturnValidationErrorForName()
        {
            var planet = StarWarsMockService.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Name, resultMapped.Name);
        }

        [Fact]
        public void ShouldReturnValidationErrorForClimate()
        {
            var planet = StarWarsMockService.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Climate, resultMapped.Climate);
        }

        [Fact]
        public void ShouldReturnValidationErrorForTerrain()
        {
            var planet = StarWarsMockService.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Terrain, resultMapped.Terrain);
        }

        #endregion

        #region AutoMapper Tester
        [Fact]
        public void ShoudReturnNoErrorForAutoMapperViewModelToDomain()
        {
            var planet = StarWarsMockService.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            var type = resultMapped.GetType();
            Assert.True(type.Equals(typeof(Planet)));
        }

        [Fact]
        public void ShoudReturnNoErrorForAutoMapperDomainToViewModel()
        {
            var planet = StarWarsMockService.GetPlanetDomain();
            var resultMapped = _mapper.Map<PlanetViewModel>(planet);
            var type = resultMapped.GetType();
            Assert.True(type.Equals(typeof(PlanetViewModel)));
        }
        #endregion

        #region Repository Tests

        [Fact]
        public void ShoudReturnAllPlanets()
        {
            var result = _mock
                .Setup(m => m.GetAllPlanets())
                .ReturnsAsync(new List<PlanetViewModel>());

            Assert.True(result.GetType() == typeof(IEnumerable<PlanetViewModel>));
        }

        [Fact]
        public void ShoudReturnPlanetById()
        {
            var planet = StarWarsMockService.GetPlanetOK();
            var result = _mock
                .Setup(m => m.GetPlanetById(It.IsAny<int>()))
                .ReturnsAsync(planet);

            var type = result.GetType();
            //Check if returns a object of type Planet
            Assert.True(type.Equals(typeof(PlanetViewModel)));
        }

        [Fact]
        public void ShouldReturnBoolWhenRemovePlanet()
        {

        }

        #endregion
    }
}

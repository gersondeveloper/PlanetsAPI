using StarWarsAPI.Domain.Entities;
using Xunit;
using FluentValidation.TestHelper;
using AutoMapper;
using StarWarsAPI.Application.AutoMapper;
using StarWarsAPI.Application.ViewModels;
using Moq;
using StarWarsAPI.Application.Interfaces;
using System.Threading.Tasks;
using StarWarsAPI.WebAPI.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace StarWarsAPI.Tests
{
    public class StarWarsUnitTests
    {
        private PlanetValidator _planetValidator;
        private IMapper _mapper;

        private Mock<IPlanetApplicationService> _mock;

        public StarWarsUnitTests()
        {
            //Initialize Mock
            _mock = new Mock<IPlanetApplicationService>();

            //Initialize PlanetValidator
            _planetValidator = new PlanetValidator();

            //Initialize AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DomainToViewModelMapping>();
            });

            _mapper = new Mapper(config);
        }

        #region Model Validator

        //Should have no errors in model
        [Fact]
        public void ShouldReturnNoValidationErrorForId()
        {
            var planet = StarWarsMockCore.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Id, resultMapped.Id);
        }

        [Fact]
        public void ShouldReturnNoValidationErrorForName()
        {
            var planet = StarWarsMockCore.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Name, resultMapped.Name);
        }

        [Fact]
        public void ShouldReturnNoValidationErrorForClimate()
        {
            var planet = StarWarsMockCore.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Climate, resultMapped.Climate);
        }

        [Fact]
        public void ShouldReturnNoValidationErrorForTerrain()
        {
            var planet = StarWarsMockCore.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldNotHaveValidationErrorFor(e => e.Terrain, resultMapped.Terrain);
        }

        //Should present errors in wrong model
        [Fact]
        public void ShoulReturnErrorForNoId()
        {
            var planet = StarWarsMockCore.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Id, resultMapped.Id);
        }

        [Fact]
        public void ShouldReturnValidationErrorForName()
        {
            var planet = StarWarsMockCore.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Name, resultMapped.Name);
        }

        [Fact]
        public void ShouldReturnValidationErrorForClimate()
        {
            var planet = StarWarsMockCore.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Climate, resultMapped.Climate);
        }

        [Fact]
        public void ShouldReturnValidationErrorForTerrain()
        {
            var planet = StarWarsMockCore.GetPlanetFail();
            var resultMapped = _mapper.Map<Planet>(planet);
            _planetValidator.ShouldHaveValidationErrorFor(e => e.Terrain, resultMapped.Terrain);
        }

        #endregion

        #region AutoMapper Tester
        [Fact]
        public void ShoudReturnNoErrorForAutoMapperViewModelToDomain()
        {
            var planet = StarWarsMockCore.GetPlanetOK();
            var resultMapped = _mapper.Map<Planet>(planet);
            var type = resultMapped.GetType();
            Assert.True(type.Equals(typeof(Planet)));
        }

        [Fact]
        public void ShoudReturnNoErrorForAutoMapperDomainToViewModel()
        {
            var planet = StarWarsMockCore.GetPlanetDomain();
            var resultMapped = _mapper.Map<PlanetViewModel>(planet);
            var type = resultMapped.GetType();
            Assert.True(type.Equals(typeof(PlanetViewModel)));
        }
        #endregion

        #region Repository Tests

        [Fact]
        public async Task ShoudReturn4Planets()
        {
            //Arrange
            var mockRepo = new Mock<IPlanetApplicationService>();
            mockRepo.Setup(repo => repo.GetAllPlanets()).ReturnsAsync(StarWarsMockCore.GetAllPlanets());
            var controller = new PlanetsController(mockRepo.Object, _mapper);

            //Act
            var result = await controller.Get();

            //Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<PlanetViewModel>>(objectResult.Value);
            Assert.Equal(4, model.Count());
        }

        [Theory]
        [InlineData(10)]
        public async Task ShoudReturnPlanetById(int id)
        {
            //Arrange
            var mockRepo = new Mock<IPlanetApplicationService>();
            mockRepo.Setup(repo => repo.GetPlanetById(id)).ReturnsAsync(StarWarsMockCore.GetPlanetOK);
            var controller = new PlanetsController(mockRepo.Object, _mapper);

            //Act
            var result = await controller.Get(id);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<PlanetViewModel>(objectResult.Value);
            Assert.Equal(10, model.Id);
        }

        [Theory]
        [InlineData(10)]
        public async Task ShouldReturnSuccessfullMessageWhenRemovePlanet(int id)
        {
            //Arrange
            var mockRepo = new Mock<IPlanetApplicationService>();
            mockRepo.Setup(repo => repo.RemovePlanet(id)).ReturnsAsync(true);
            var controller = new PlanetsController(mockRepo.Object, _mapper);

            //Act
            var result = await controller.Delete(id);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var strResponse = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal("Planet was successfull deleted!", strResponse);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public async Task ShouldReturnSuccessfullMessageWhenCreatePlanet(int id, string name, string climate, string terrain, int appearances)
        {
            //Arrange
            var planet = new Planet() { Id = id, Name = name, Climate = climate, Terrain = terrain, AppearanceInMovies = appearances };
            var mockRepo = new Mock<IPlanetApplicationService>();
            mockRepo.Setup(repo => repo.CreatePlanet(planet)).ReturnsAsync(true);
            var controller = new PlanetsController(mockRepo.Object, _mapper);

            //Act
            var result = await controller.Post(planet);

            //Assert
            var objectResult = Assert.IsType<OkObjectResult>(result);
            var strResponse = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal("Planet successfull created!", strResponse);
        }

        public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] { 1, "Planet Name 1", "Planet Climate 1", "Planet Terrain 1", 10 },
        };

        #endregion
    }
}

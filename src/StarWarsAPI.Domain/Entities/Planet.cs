using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StarWarsAPI.Domain.Entities
{
    [BsonIgnoreExtraElements]
    public class Planet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public int AppearanceInMovies { get; set; }
    }

    public class PlanetValidator : AbstractValidator<Planet>
    {
        public PlanetValidator()
        {
            //TODO: Revisar a quantidade de caracteres para cada propriedade

            RuleFor(x => x.Name)
                .NotNull()
                .Length(2, 30)
                .WithMessage("Planet must have between 2 and 30 characters");

            RuleFor(x => x.Climate)
                .NotNull()
                .Length(2, 30)
                .WithMessage("Climate must have between 2 and 30 characters");

            RuleFor(x => x.Terrain)
                .NotNull()
                .Length(2, 30)
                .WithMessage("Terrain must have between 2 and 30 characters");
        }
    }
}

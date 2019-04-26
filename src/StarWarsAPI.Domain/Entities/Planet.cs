using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StarWarsAPI.Domain.Entities
{
    public class Planet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Climate")]
        public string Climate { get; set; }
        [BsonElement("Terrain")]
        public string Terrain { get; set; }
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

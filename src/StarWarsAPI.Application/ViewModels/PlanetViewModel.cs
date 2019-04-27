using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StarWarsAPI.Application.ViewModels
{
    
    public sealed class PlanetViewModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("climate")]
        public string Climate { get; set; }
        [BsonElement("terrain")]
        public string Terrain { get; set; }
        public int AppearanceInMovies { get; set; }
    }
}

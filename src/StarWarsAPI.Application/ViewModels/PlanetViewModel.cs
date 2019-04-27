using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StarWarsAPI.Application.ViewModels
{
    public class PlanetViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Climate { get; set; }

        public string Terrain { get; set; }

        public int AppearanceInMovies { get; set; }

    }
}

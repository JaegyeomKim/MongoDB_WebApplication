using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace MongoDB_WebApplication.Models
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;
        [BsonElement("imageURL")]
        public string ImageURL { get; set; } = String.Empty;
        [BsonElement("link")]
        public string Link { get; set; } = String.Empty;
        [BsonElement("price")]
        public string Price { get; set; } = String.Empty;

    }
}

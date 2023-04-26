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
        [BsonElement("productName")]
        public string ProductName { get; set; } = String.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;
        [BsonElement("imageName")]
        public string ImageName { get; set; } = String.Empty;

        public IFormFile ?ImageFile { get; set; }
        public string ?ImageSrc { get; set; }

    }
}

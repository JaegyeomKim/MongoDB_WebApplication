using MongoDB_WebApplication.Models;
using MongoDB.Driver;
namespace MongoDB_WebApplication.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IMongoCollection<Product> _product;

        public ProductServices(IProjectStoreDatabaseSetting settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _product = database.GetCollection<Product>(settings.ProjectCollectionName);
        }
        public Product Create(Product product)
        {
            _product.InsertOne(product);
            return product;
        }

        public List<Product> Get()
        {
            return _product.Find(product => true).ToList();
        }

        public Product Get(string id)
        {
            return _product.Find(product => product.Id == id).FirstOrDefault();
        }

        public void Remove(string id)
        {
            _product.DeleteOne(product => product.Id == id);
        }

        public void Update(string id, Product product)
        {
            _product.ReplaceOne(product => product.Id == id, product);
        }
    }
}

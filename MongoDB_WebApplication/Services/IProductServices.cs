using MongoDB_WebApplication.Models;

namespace MongoDB_WebApplication.Services
{
    public interface IProductServices
    {
        List<Product> Get();
        Product Get(string id);
        Product Create(Product product);
        void Update(string id, Product product);
        void Remove(string id);

    }
}

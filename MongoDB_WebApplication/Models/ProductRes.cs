namespace MongoDB_WebApplication.Models
{
    public class ProductRes
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB_WebApplication.Models;
using MongoDB_WebApplication.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDB_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {

        private readonly IProductServices productService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PagesController(IProductServices productService, IWebHostEnvironment hostEnvironment)
        {
            this.productService = productService;
            this._hostEnvironment = hostEnvironment;
        }

        // GET api/<PagesController>/5
        [HttpGet("{page}")]
        public ActionResult<List<Product>> GetProducts(int page)
        {
            if (productService == null)
                return NotFound();

            var pageResult = 3f;
            var pageCount = Math.Ceiling(productService.Get().Count() / pageResult);

            var products = this.productService.Get()
                .Skip((page - 1) * (int)pageResult)
                .Take((int)pageResult)
                .ToList();

            var response = new ProductRes
            {
                Products = products,
                CurrentPage = page,
                Pages = (int)pageCount
            };
            return Ok(response);
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using MongoDB_WebApplication.Models;
using MongoDB_WebApplication.Services;
using Microsoft.AspNetCore.Hosting;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MongoDB_WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices productService;
        private readonly IWebHostEnvironment _hostEnvironment;


        public ProductController(IProductServices productService, IWebHostEnvironment hostEnvironment)
        {
            this.productService = productService;
            this._hostEnvironment = hostEnvironment;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            var products = productService.Get();
            foreach (var item in products)
            {
                item.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, item.ImageName);
            }

            return products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> Get(string id)
        {
            var product = productService.Get(id);
            if (product == null)
            {
                return NotFound($"Product with ID = {id} not found");
            }
            product.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, product.ImageName);
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Post([FromBody]Product product)
        {
            //product.ImageName = SaveImage(product.ImageFile);
            productService.Create(product);
            return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
        }


        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Product product)
        {
            var existingProduct = productService.Get(id);
            
            if(existingProduct != null)
            {
                return NotFound($"Product with ID = {id} not found");
            }

            productService.Update(id, product);

            return NoContent();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var product = productService.Get(id);

            if (product != null)
            {
                return NotFound($"Product with ID = {id} not found");
            }

            productService.Remove(product.Id);

            return Ok($"Product with ID = {id} deleted");
        }

        [NonAction]
        public string SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        //[HttpGet("{page}")]
        //public ActionResult<List<Product>> GetProducts(int page)
        //{
        //    if (productService == null)
        //        return NotFound();

        //    var pageResult = 3f;
        //    var pageCount = Math.Ceiling(productService.Get().Count() / pageResult);

        //    var products = this.productService.Get()
        //        .Skip((page - 1) * (int)pageResult)
        //        .Take((int)pageResult)
        //        .ToList();

        //    var response = new ProductRes
        //    {
        //        Products = products,
        //        CurrentPage = page,
        //        Pages = (int)pageCount
        //    };
        //    return Ok(response);
        //}

    }

}

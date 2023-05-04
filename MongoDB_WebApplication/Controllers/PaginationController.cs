using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB_WebApplication.Models;
using MongoDB_WebApplication.Services;

namespace MongoDB_WebApplication.Controllers
{
    public class PaginationController : Controller
    {
        private readonly IProductServices productService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public PaginationController(IProductServices productService, IWebHostEnvironment hostEnvironment)
        {
            this.productService = productService;
            this._hostEnvironment = hostEnvironment;
        }
        // GET: PaginationController
        public ActionResult Index()
        {
            return View();
        }

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

        // GET: PaginationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaginationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaginationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PaginationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaginationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaginationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

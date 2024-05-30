using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101_Demo.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            string hashData = Guid.NewGuid().ToString();
            ViewBag.Hash = hashData;
            if (id is null) return BadRequest();
            Product product = await _productService.GetByIdWithAllDatasAsync((int)id);
            if (product is null) return NotFound();
            return View(product);
        }


    }
}

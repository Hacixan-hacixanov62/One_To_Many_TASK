using Fiorello_PB101_Demo.Data;
using Fiorello_PB101_Demo.Helpers;
using Fiorello_PB101_Demo.Helpers.Extensions;
using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.Services.Interfaces;
using Fiorello_PB101_Demo.ViewModels.Baskets;
using Fiorello_PB101_Demo.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiorello_PB101_Demo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await _productService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _productService.GetMappedDatas(products);

            int totalPage = await GetPageCountAsync(4);

            Paginate<ProductVM> paginateDatas = new(mappedDatas, totalPage, page);

            return View(paginateDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }

        public async Task<IActionResult> Detial(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);

            if(existProduct is null) return NotFound();

            List<ProductImageVM > images = new ();

            foreach(var item in existProduct.ProductImages)
            {
                images.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }

            ProductDetialVM response = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Category = existProduct.Category.Name,
                Price = existProduct.Price,
                Images = images
            };

            return View(response);
            

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM request)
        {

            ViewBag.categories = await _categoryService.GetAllSelectedAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach(var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Images size must be max 500 KB");
                    return View();
                }
                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                    return View();
                }
            }

            List<ProductImage> images = new();

            foreach (var item in request.Images)
            {
                string fileName = $"{Guid.NewGuid()} - {item.FileName}";
                string path = _webHostEnvironment.GenerateFilePath("img", fileName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new ProductImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;


            Product product = new()
            {
                Name = request.Name,
                Description = request.Description,
                CategoryId = request.CategoryId,
                Price = decimal.Parse(request.Price.Replace(".",",")),
                 ProductImages = images
            };

            await _productService.CreateAsync(product);

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {

            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);

            if (existProduct is null) return NotFound();

            foreach (var item in existProduct.ProductImages)
            {
                string path = _webHostEnvironment.GenerateFilePath("img", item.Name);
                path.DeleteFileFromLocal();

            }

            await _productService.DeleteAsync(existProduct);
            return RedirectToAction(nameof(Index));

        }
    }

}





    
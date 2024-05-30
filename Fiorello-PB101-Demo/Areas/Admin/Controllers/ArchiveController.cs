using Fiorello_PB101_Demo.Data;
using Fiorello_PB101_Demo.Helpers;
using Fiorello_PB101_Demo.Services.Interfaces;
using Fiorello_PB101_Demo.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101_Demo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        public ArchiveController(ICategoryService categoryService,
                                 AppDbContext context)
        {
           _categoryService = categoryService;
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> CategoryArchive(int page = 1)
        {
            var archives = await _categoryService.GetAllPaginateArchiveAsync(page, 2);

            var mappedDatas = _categoryService.GetMappedAddDatas(archives);

            int totalPage = await GetPageCountAsync(2);

            Paginate<CategoryArchiveVM> paginateDatas = new(mappedDatas, totalPage, page);

            return View(paginateDatas);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryUnarchive(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _categoryService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            category.SoftDeleted = false;

            await _context.SaveChangesAsync();

            return Ok(category);
        }


        private async Task<int> GetPageCountAsync(int take)
        {
            int CategoryCount = await _categoryService.GetAllCountAsync();
            return (int)Math.Ceiling((decimal)CategoryCount / take);
        }

        //[HttpGet]
        //public async Task<IActionResult> Index(int page = 1)
        //{
        //    var categories = await _categoryService.GetAllPaginateAsync(page, 3);

        //    var mappedDatas = _categoryService.GetMappedDatas(categories);

        //    int totalPage = await GetPageCountAsync(3);

        //    Paginate<CategoryProductVM> paginateDatas = new(mappedDatas, totalPage, page);

        //    return View(paginateDatas);
        //}

        //private async Task<int> GetPageCountAsync(int take)
        //{
        //    int CategoryCount = await _categoryService.GetCountAsync();
        //    return (int)Math.Ceiling((decimal)CategoryCount / take);
        //}



    }
}

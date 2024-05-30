using Fiorello_PB101_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101_Demo.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _blogService.GetAllAsync());
        }
    }
}

using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.ViewModels.Blog;
using Fiorello_PB101_Demo.ViewModels.Expert;

namespace Fiorello_PB101_Demo.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<BlogVM> Blogs { get; set; }
        public IEnumerable<ExpertVM> Experts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}

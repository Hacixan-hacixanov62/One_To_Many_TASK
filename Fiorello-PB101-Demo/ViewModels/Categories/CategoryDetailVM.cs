namespace Fiorello_PB101_Demo.ViewModels.Categories
{
    public class CategoryDetailVM
    {
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public ICollection<string> Products { get; set; }
        public int ProductCount { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Fiorello_PB101_Demo.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}

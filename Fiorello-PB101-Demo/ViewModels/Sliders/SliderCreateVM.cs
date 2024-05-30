using System.ComponentModel.DataAnnotations;

namespace Fiorello_PB101_Demo.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        public List<IFormFile> Images { get; set; }

    }
}

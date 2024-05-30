using Fiorello_PB101_Demo.Models;

namespace Fiorello_PB101_Demo.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<SliderInfo> GetSliderInfoAsync();

    }
}

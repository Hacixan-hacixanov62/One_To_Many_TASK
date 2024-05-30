using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101_Demo.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;
        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderDatas = new SliderVMC
            {
                Sliders = await _sliderService.GetAllAsync(),
                SliderInfo = await _sliderService.GetSliderInfoAsync(),
            };

            return await Task.FromResult(View(sliderDatas));
        }
    }

    public class SliderVMC
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public SliderInfo SliderInfo { get; set; }
    }
}

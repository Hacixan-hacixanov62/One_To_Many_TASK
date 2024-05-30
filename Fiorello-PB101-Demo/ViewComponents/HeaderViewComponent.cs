
using Fiorello_PB101_Demo.Services.Interfaces;
using Fiorello_PB101_Demo.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiorello_PB101_Demo.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _accessor;
        public HeaderViewComponent(ISettingService settingService,
                                   IHttpContextAccessor accessor)
        {
            _settingService = settingService;
            _accessor = accessor;   
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketVM> basketDatas = new();

            if (_accessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
         

            Dictionary<string, string> settings = await _settingService.GetSettingAsync();

            HeaderVM response = new()
            {
                Settings = settings,
                BaskeCount = basketDatas.Sum(m=>m.Count),
                BasketTotalPrice = basketDatas.Sum(m=>m.Count * m.Price)

            };


            return await Task.FromResult(View(response));
        }

    }

    public class HeaderVM
    {
        public  int BaskeCount { get; set; }
        public decimal BasketTotalPrice { get; set; }
        public Dictionary<string,string> Settings{ get; set; }
    }


}

using Fiorello_PB101_Demo.Data;
using Fiorello_PB101_Demo.Models;
using Fiorello_PB101_Demo.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101_Demo.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<SliderInfo> GetSliderInfoAsync()
        {
            return await _context.SliderInfos.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
        }
    }
}

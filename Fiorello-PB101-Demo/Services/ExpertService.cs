using Fiorello_PB101_Demo.Data;
using Fiorello_PB101_Demo.Services.Interfaces;
using Fiorello_PB101_Demo.ViewModels.Expert;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101_Demo.Services
{
    public class ExpertService : IExpertService
    {
        private readonly AppDbContext _context;

        public ExpertService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ExpertVM>> GetAllAsync()
        {
            var experts = await _context.Experts.ToListAsync();

            return experts.Select(m => new ExpertVM
            {
                FullName = m.FullName,
                Position = m.Position,
                Image = m.Image
            });
        }
    }
}

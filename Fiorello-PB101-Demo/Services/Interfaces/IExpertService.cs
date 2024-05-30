using Fiorello_PB101_Demo.ViewModels.Expert;

namespace Fiorello_PB101_Demo.Services.Interfaces
{
    public interface IExpertService
    {
        Task<IEnumerable<ExpertVM>> GetAllAsync();
    }
}

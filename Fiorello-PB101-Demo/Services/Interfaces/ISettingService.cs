namespace Fiorello_PB101_Demo.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetSettingAsync();
    }
}

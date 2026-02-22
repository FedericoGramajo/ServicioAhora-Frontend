using ClientLibrary.Models.Landing;

namespace ClientLibrary.Services.Contracts
{
    public interface IServiceHistoryService
    {
        Task<List<ServiceHistoryItem>> GetHistoryAsync();
    }
}

using System.Threading.Tasks;

namespace MobiHymnMaui.Models
{
    public interface IRemoteConfigurationService
    {
        Task FetchAndActivateAsync();
        Task<TInput> GetAsync<TInput>(string key);
    }
}

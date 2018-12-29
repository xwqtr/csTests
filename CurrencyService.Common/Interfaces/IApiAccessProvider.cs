
namespace CurrencyService.Common.Interfaces
{
    using System.Threading.Tasks;

    public interface IApiAccessProvider
    {
        Task<T> GetData<T>(string uri, string[] headers = null);
    }
}

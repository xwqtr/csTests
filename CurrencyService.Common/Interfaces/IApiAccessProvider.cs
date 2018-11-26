
namespace CurrencyService.Common.Interfaces
{
    using System.Threading.Tasks;

    public interface IApiAccessProvider
    {
        T GetData<T>(string uri);
    }
}

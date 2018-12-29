
namespace CurrencyService.Common.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICurrencyWebService
    {
        Task<T> GetCurrencyExchangeInfo<T>();

        Task<IEnumerable<T>> GetHistoricalTrades<T>(string currencyName,string currencyToConvert) where T : IHistoricalTrade;
    }
}
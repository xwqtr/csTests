
namespace CurrencyService.Common.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICurrencyWebService
    {
        T GetCurrencyExchangeInfo<T>();

        IEnumerable<T> GetHistoricalTrades<T>(string currencyName,string currencyToConvert) where T : IHistoricalTrade;
    }
}
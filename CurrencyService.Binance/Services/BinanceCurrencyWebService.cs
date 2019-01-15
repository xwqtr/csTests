namespace CurrencyService.Binance.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CurrencyService.Common.Interfaces;
    using CurrencyService.Binance.Models;
    using System.Threading.Tasks;

    public class BinanceCurrencyWebService : ICurrencyWebService
    {
        private readonly IApiAccessProvider _aap;

        private readonly IApiAccessParameters _apiAccessParameters;
        public BinanceCurrencyWebService(IApiAccessProvider aap) 
        {
            _aap = aap;
            _apiAccessParameters = new BinanceApiAccessParameters();
        }

        public async Task<T> GetCurrencyExchangeInfo<T>()
        {
            return await _aap.GetData<T>("exchangeInfo");
        }

        public async Task<IEnumerable<T>> GetHistoricalTrades<T>(string currencyName, string currencyToConvert) where T : IHistoricalTrade
        {
            try
            {
                var data = await _aap.GetData<IEnumerable<BinanceModel.HistoricalTrade>>($"{_apiAccessParameters.baseAddress}historicalTrades?symbol={currencyName}{currencyToConvert}", _apiAccessParameters.headers);
                
                foreach (var x in data)
                {
                    x.CurrencyName = currencyName;
                    x.CurrencyToConvertName = currencyToConvert;
                }
                return data.Cast<T>();
            }
            catch
            {
                var data = await _aap.GetData<IEnumerable<BinanceModel.HistoricalTrade>>($"{_apiAccessParameters.baseAddress}historicalTrades?symbol={currencyToConvert}{currencyName}", _apiAccessParameters.headers);

                foreach (var x in data)
                {
                    x.CurrencyName = currencyToConvert;
                    x.CurrencyToConvertName = currencyName;
                }
                return data.Cast<T>();
            }
            
        }

        
    }
}

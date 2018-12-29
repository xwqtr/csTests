

namespace CurrencyService.Poloniex
{
    using CommonApiAccessProvider;
    using CurrencyService.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PoloniexCurrencyWebService : ICurrencyWebService
    {
        private readonly IApiAccessProvider _aap;
        private readonly IApiAccessParameters _apiAccessParameters;
        public PoloniexCurrencyWebService(IApiAccessProvider aap)
        {
            _aap = aap;
            _apiAccessParameters = new PoloniexApiAccessParameters();
        }
        public async Task<T> GetCurrencyExchangeInfo<T>()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetHistoricalTrades<T>(string currencyName,string currencyToConvert) where T : IHistoricalTrade
        {
            IEnumerable<IHistoricalTrade> result;
            try
            {
                var data = await _aap.GetData<IEnumerable<Models.Poloniex.HistoricalTrade>>($"{_apiAccessParameters.baseAddress}public?command=returnTradeHistory&currencyPair={currencyName}_{currencyToConvert}", _apiAccessParameters.headers);

                foreach (var x in data)
                {
                    x.CurrencyName = currencyName;
                    x.CurrencyToConvertName = currencyToConvert;
                }
                result = data;
            }
            catch
            {
                var data = await _aap.GetData<IEnumerable<Models.Poloniex.HistoricalTrade>>($"{_apiAccessParameters.baseAddress}public?command=returnTradeHistory&currencyPair={currencyToConvert}_{currencyName}", _apiAccessParameters.headers);
                foreach (var x in data)
                {
                    x.CurrencyName = currencyToConvert;
                    x.CurrencyToConvertName = currencyName;
                }
                result = data;
            }


            return result.Cast<T>();
        }
    }
}

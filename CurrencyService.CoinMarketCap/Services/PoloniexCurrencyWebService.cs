

namespace CurrencyService.Poloniex
{
    using CommonApiAccessProvider.Abstracts;
    using CurrencyService.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class PoloniexCurrencyWebService : ICurrencyWebService
    {
        private const string _key = "4eea0588-3553-40a2-9d46-6b3e7f9c535c";


        private readonly BaseApiAccessProvider _bah;
        public PoloniexCurrencyWebService(BaseApiAccessProvider bah)
        {
            _bah = bah;
        }
        public T GetCurrencyExchangeInfo<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetHistoricalTrades<T>(string currencyName,string currencyToConvert) where T : IHistoricalTrade
        {
            return _bah
                .GetData<IEnumerable<Models.Poloniex.HistoricalTrade>>($"public?command=returnTradeHistory&currencyPair={currencyName}_{currencyToConvert}")
                .Cast<T>();
        }
    }
}

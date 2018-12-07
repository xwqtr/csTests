using CommonApiAccessProvider.Abstracts;
using CurrencyService.Common.Interfaces;
using System;
using System.Collections.Generic;

namespace CurrencyService.Services.CoinMarketCap
{
    public class CoinMarketCurrencyWebService : ICurrencyWebService
    {
        private const string _key = "4eea0588-3553-40a2-9d46-6b3e7f9c535c";


        private readonly BaseApiAccessProvider _bah;
        public CoinMarketCurrencyWebService(BaseApiAccessProvider bah)
        {
            _bah = bah;
        }
        public T GetCurrencyExchangeInfo<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetHistoricalTrades<T>(string currencyName) where T : IHistoricalTrade
        {
            return _bah
                .GetData<IEnumerable<BinanceModel.HistoricalTrade>>($"historicalTrades?symbol={currencyName}&fromId={fromId}&limit={limit}")
                .Cast<T>();
        }
    }
}

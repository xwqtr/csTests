namespace CurrencyService.Binance.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using CurrencyService.Common.Interfaces;
    using CurrencyService.Binance.Models;
    using System.Threading.Tasks;
    using CommonApiAccessProvider.Abstracts;
    public class BinanceCurrencyWebService : ICurrencyWebService
    {
        private readonly BaseApiAccessProvider _bah;
        public BinanceCurrencyWebService(BaseApiAccessProvider bah) 
        {
            _bah = bah;
        }

        public T GetCurrencyExchangeInfo<T>()
        {
            return _bah.GetData<T>("exchangeInfo");
        }

        public IEnumerable<T> GetAllHistoricalTrades<T>(string currencyName) where T : IHistoricalTrade
        {
            //not used because API does not allow too many request
            int from = 1;
            var max = _bah
                .GetData<IEnumerable<BinanceModel.HistoricalTrade>>($"historicalTrades?symbol={currencyName}").Select(x => x.id).Max();
            List<int> ranges = new List<int>();
            while (from < max)
            {
                ranges.Add(from);
                from += 1000;
            }
            var result = new List<T>();
            ranges.AsParallel().ForAll(x => { result.AddRange(this.GetHistoricalTrades<T>(currencyName, x)); });
            return result;
        }

        public IEnumerable<T> GetHistoricalTrades<T>(string currencyName) where T : IHistoricalTrade
        {
            return _bah.GetData<IEnumerable<BinanceModel.HistoricalTrade>>($"historicalTrades?symbol={currencyName}").Cast<T>();
        }

        private IEnumerable<T> GetHistoricalTrades<T>(string currencyName, long fromId,int limit = 1000) where T : IHistoricalTrade
        {
            return _bah
                .GetData<IEnumerable<BinanceModel.HistoricalTrade>>($"historicalTrades?symbol={currencyName}&fromId={fromId}&limit={limit}")
                .Cast<T>();
        }

        
    }
}

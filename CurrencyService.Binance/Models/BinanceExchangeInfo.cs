namespace CurrencyService.Binance.Models
{
    using CurrencyService.Common.Interfaces;
    using System.Collections.Generic;
    public class BinanceModel
    {
        public class HistoricalTrade : IHistoricalTrade
        {
            public int id { get; set; }
            public decimal Price { get; set; }
            public decimal qty { get; set; }
            public long Time { get; set; }
            public bool isBuyerMaker { get; set; }
            public bool isBestMatch { get; set; }
            public decimal Count { get  { return qty; } set { qty = value; } }

            public bool IsBought { get { return isBuyerMaker; } set { isBuyerMaker = value; } }

            public string CurrencyName { get; set ; }
            public string CurrencyToConvertName { get; set; }
            public bool Equals(IHistoricalTrade other)
            {
                throw new System.NotImplementedException();
            }
        }

        public class RateLimit
        {
            public string rateLimitType { get; set; }
            public string interval { get; set; }
            public int intervalNum { get; set; }
            public int limit { get; set; }
        }

        public class Filter
        {
            public string filterType { get; set; }
            public string minPrice { get; set; }
            public string maxPrice { get; set; }
            public string tickSize { get; set; }
            public string minQty { get; set; }
            public string maxQty { get; set; }
            public string stepSize { get; set; }
            public string minNotional { get; set; }
            public bool? applyToMarket { get; set; }
            public int? avgPriceMins { get; set; }
            public int? limit { get; set; }
            public int? maxNumAlgoOrders { get; set; }
        }

        public class Symbol
        {
            public string symbol { get; set; }
            public string status { get; set; }
            public string baseAsset { get; set; }
            public int baseAssetPrecision { get; set; }
            public string quoteAsset { get; set; }
            public int quotePrecision { get; set; }
            public List<string> orderTypes { get; set; }
            public bool icebergAllowed { get; set; }
            public List<Filter> filters { get; set; }
        }

        public class ExchangeInfo
        {
            public string timezone { get; set; }
            public long serverTime { get; set; }
            public List<RateLimit> rateLimits { get; set; }
            public List<object> exchangeFilters { get; set; }
            public List<Symbol> symbols { get; set; }
        }
    }
}

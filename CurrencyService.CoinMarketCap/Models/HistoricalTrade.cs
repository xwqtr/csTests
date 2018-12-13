namespace CurrencyService.CoinMarketCap.Models
{
    using CurrencyService.Common.Interfaces;
    using System;
    public class CoinMarketCap
    {
        public class HistoricalTrade : IHistoricalTrade
        {
            public string Price { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public long Time { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public decimal Count { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public bool IsBought { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public bool Equals(IHistoricalTrade other)
            {
                throw new NotImplementedException();
            }
        }
    }

}

namespace CurrencyService.Poloniex.Models
{
    using CurrencyService.Common.Interfaces;
    using System;
    public class Poloniex
    {
        public class HistoricalTrade : IHistoricalTrade
        {
            public decimal Price { get => rate; set { } }
            public long Time { get => date.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).Ticks/10000; set { } }
            public decimal Count { get => amount; set { } }
            public bool IsBought { get { return this.type == "buy"; } set { } }
            public string CurrencyName { get ; set ; }
            public string CurrencyToConvertName { get ; set; }


            public int globalTradeID { get; set; }
            public int tradeID { get; set; }
            public DateTime date { get; set; }
            public string type { get; set; }
            public decimal rate { get; set; }
            public decimal amount { get; set; }
            public decimal total { get; set; }

            public bool Equals(IHistoricalTrade other)
            {
                throw new NotImplementedException();
            }
        }
    }

}

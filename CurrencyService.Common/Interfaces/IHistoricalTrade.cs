

namespace CurrencyService.Common.Interfaces
{
    using System;
    public interface IHistoricalTrade : IEquatable<IHistoricalTrade>
    {
        string Price { get; set; }
        long Time { get; set; }
        decimal Count { get; set; }

        bool IsBought { get; set; }
    }
}
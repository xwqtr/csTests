

namespace CurrencyService.Common.Interfaces
{
    using System;
    public interface IHistoricalTrade : IEquatable<IHistoricalTrade>
    {
        decimal Price { get; set; }
        long Time { get; set; }
        decimal Count { get; set; }

        bool IsBought { get; set; }

        string CurrencyName { get; set; }

        string CurrencyToConvertName { get; set; }
    }
}
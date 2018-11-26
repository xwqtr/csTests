namespace CurrencyService.Common.Interfaces
{
    public interface IHistoricalTrade
    {
        string Price { get; set; }
        long Time { get; set; }
        decimal Count { get; set; }

        bool IsBought { get; set; }
    }
}
namespace CurrencyService.BackgroundService.Common
{
    public interface IBackgroundWorkerConfiguration
    {
         int SecondsInterval { get; set; }
    }
}
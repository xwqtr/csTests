namespace CurrencyService.Common.Interfaces
{
    public interface IApiAccessParameters
    {
         string baseAddress { get; set; }
         string[] headers { get; set; }

    }
}


namespace ConsoleApp6
{
    using ConsoleApp6.Abstracts;
    using ConsoleApp6.Models;
    using ConsoleApp6.Services;
    using Microsoft.Extensions.DependencyInjection;
    class Program
    {
        static void Main(string[] args)
        {
            var sp = GetSP();
            var wws = sp.GetService<WeatherWebService>();
            var data = wws.GetWeatherByCity<WeatherData.RootObject>("Kazan");
        }


        public static ServiceProvider GetSP() {
            return new ServiceCollection()
                .AddTransient<BaseApiAccessProvider, WebRequestBasedUriAccess>(x => new WebRequestBasedUriAccess("https://api.openweathermap.org/data/2.5/", "5b99c9b284df2d7a1b1a3805ced9ec7f"))

                .AddTransient<BaseApiAccessProvider,WebClientBasedUriAccess>(x=> new WebClientBasedUriAccess("https://api.openweathermap.org/data/2.5/", "5b99c9b284df2d7a1b1a3805ced9ec7f"))
                .AddSingleton<WeatherWebService>()
                .BuildServiceProvider();
        }
    }
}

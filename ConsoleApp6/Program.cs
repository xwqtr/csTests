using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            var sp = GetSP();
            var wws = sp.GetService<WeatherWebService>();
            var data = wws.GetDataWebClient<WeatherData.RootObject>("/data/2.5/weather?id=2172797&appid=b6907d289e10d714a6e88b30761fae22");
        }


        public static ServiceProvider GetSP() {

            return new ServiceCollection().AddSingleton(new WeatherWebService("https://api.openweathermap.org")).BuildServiceProvider();
        }
    }
}

using ConsoleApp6.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6.Services
{
    class WeatherWebService
    {

        
        private readonly BaseApiAccessProvider _bah;
        public WeatherWebService(BaseApiAccessProvider bah) 
        {
            _bah = bah;
        }

        public T GetWeatherByCity<T>(string cityName)
        {
           return  _bah.GetData<T>($"weather?q={cityName}");
        }
        

    }
}

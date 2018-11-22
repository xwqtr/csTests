using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class WeatherWebService : BaseWebService
    {
        public WeatherWebService(string address) : base(new WebClient()
        {
            BaseAddress = address
        })
        {

        }

    }
}

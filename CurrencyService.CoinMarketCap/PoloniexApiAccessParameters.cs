using CommonApiAccessProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyService.Poloniex
{
    public class PoloniexApiAccessParameters : IApiAccessParameters
    {
        public string baseAddress { get => "https://poloniex.com/"; set { } }
        public string[] headers { get => null; set { } }
    }
}

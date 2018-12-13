using CommonApiAccessProvider;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyService.Binance
{
    public class BinanceApiAccessParameters : IApiAccessParameters
    {
        public string baseAddress { get => "https://api.binance.com/api/v1/"; set { } }
        public string[] headers { get => new string[] { "X-MBX-APIKEY:FXsYKj85DJeUrnT8E80rBMFx7DuN3abBr29E1Q5pvUa7sCmyw8VoSG0nlz4InhLN" }; set { } }
    }
}

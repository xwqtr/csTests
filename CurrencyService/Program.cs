namespace CurrencyService
{
    using CurrencyService.Abstracts;
    using CurrencyService.ApiAccess.Services;
    using CurrencyService.Binance.Models;
    using CurrencyService.Binance.Services;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using CurrencyService.DB;
    using CurrencyService.DAL;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.SqlServer;

    class Program
    {
        static void Main(string[] args)
        {
            var sp = GetSP();
            
            var wws = sp.GetService<BinanceCurrencyWebService>();
            var data = wws.GetHistoricalTrades<BinanceModel.HistoricalTrade>("ETHBTC");
            var dws = sp.GetService<DbWriteService>();
            dws.WriteHistoricalTrades(data);
        }


        public static ServiceProvider GetSP() {
            return new ServiceCollection()
                .AddTransient<BaseApiAccessProvider, WebRequestBasedApiAccess>(x => new WebRequestBasedApiAccess("https://api.binance.com/api/v1/", new string[] { "X-MBX-APIKEY:FXsYKj85DJeUrnT8E80rBMFx7DuN3abBr29E1Q5pvUa7sCmyw8VoSG0nlz4InhLN" }))
               // .AddTransient<BaseApiAccessProvider,WebClientBasedApiAccess>(x=> new WebClientBasedApiAccess("https://api.binance.com/api/v1/", new string[] { "X-MBX-APIKEY:FXsYKj85DJeUrnT8E80rBMFx7DuN3abBr29E1Q5pvUa7sCmyw8VoSG0nlz4InhLN" }))
                .AddSingleton<BinanceCurrencyWebService>()
                
                .AddDbContext<CurrencyDbContext>(x=>x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .AddSingleton<DbWriteService>()
                .BuildServiceProvider();
        }
    }
}

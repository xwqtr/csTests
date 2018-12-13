namespace CurrencyServiceClient
{
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using CurrencyService.DB;
    using CurrencyService.DAL;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.SqlServer;
    using System.Linq;
    using System;
    using CommonApiAccessProvider.ApiAccess;
    using CommonApiAccessProvider.Abstracts;
    using CurrencyService.Common.Interfaces;
    using GenericLibraryParser;
    using System.IO;
    using CommonApiAccessProvider;
    class Program
    {
        static void Main(string[] args)
        {
            var c1 = "BTC";
            var c2 = "ETH";
            var sp = GetSP();
            var wws = sp.GetService<ICurrencyWebService>();
            var data = wws.GetHistoricalTrades<IHistoricalTrade>(c1, c2).ToList();
            data.ForEach(x => { x.CurrencyName = c1; x.CurrencyToConvertName = c2; });
            var dws = sp.GetService<DbWriteService>();
            dws.WriteHistoricalTrades(data);
            var drs = sp.GetService<DbReadService>();
            var hTradesFromDatabase = drs.GetHistoricalTrades();
            var getMaxPrice = drs.GetHistoricalTrades().Max(x => x.Price);
        }


        // https://api.binance.com/api/v1/", new string[] { "X-MBX-APIKEY:FXsYKj85DJeUrnT8E80rBMFx7DuN3abBr29E1Q5pvUa7sCmyw8VoSG0nlz4InhLN"


        public static ServiceProvider GetSP()
        {
            return new ServiceCollection()
                .AddTransientFromLibrary<ICurrencyWebService>(Directory.GetCurrentDirectory() + "\\CurrencyServices")
                .AddTransientFromLibrary<IApiAccessParameters>(Directory.GetCurrentDirectory() + "\\CurrencyServices")
                .AddTransient<BaseApiAccessProvider, WebRequestBasedApiAccess>()
                .AddScoped<WebRequestBasedApiAccess>()
                // .AddTransient<BaseApiAccessProvider,WebClientBasedApiAccess>(x=> new WebRequestBasedApiAccess("https://pro-api.coinmarketcap.com/v1/", new string[] { "X-CMC_PRO_API_KEY:4eea0588-3553-40a2-9d46-6b3e7f9c535c" }))
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .AddSingleton<DbWriteService>()
                .AddSingleton<DbReadService>()
                .BuildServiceProvider();
        }

       

       

    }
}

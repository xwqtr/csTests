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

    class Program
    {
        static void Main(string[] args)
        {
            
            var sp = GetSP();
            var wws = sp.GetService<ICurrencyWebService>();
            var data = wws.GetHistoricalTrades<IHistoricalTrade>("ETHBTC");
            var dws = sp.GetService<DbWriteService>();
            dws.WriteHistoricalTrades(data);
            var drs = sp.GetService<DbReadService>();
            var hTradesFromDatabase = drs.GetHistoricalTrades();
            var getMaxPrice = drs.GetHistoricalTrades().Max(x => x.Price);
        }





        public static ServiceProvider GetSP()
        {


            var sc = new ServiceCollection();
            AddCurrencyServices(sc);

            return sc
                .AddTransient<BaseApiAccessProvider, WebRequestBasedApiAccess>(x => new WebRequestBasedApiAccess("https://api.binance.com/api/v1/", new string[] { "X-MBX-APIKEY:FXsYKj85DJeUrnT8E80rBMFx7DuN3abBr29E1Q5pvUa7sCmyw8VoSG0nlz4InhLN" }))
                // .AddTransient<BaseApiAccessProvider,WebClientBasedApiAccess>(x=> new WebClientBasedApiAccess("https://api.binance.com/api/v1/", new string[] { "X-MBX-APIKEY:FXsYKj85DJeUrnT8E80rBMFx7DuN3abBr29E1Q5pvUa7sCmyw8VoSG0nlz4InhLN" }))
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .AddSingleton<DbWriteService>()
                .AddSingleton<DbReadService>()
                .BuildServiceProvider();
        }

        private static void AddCurrencyServices(IServiceCollection sc)
        {
            var loader = new Loader(Directory.GetCurrentDirectory()+"\\CurrencyServices");
            var type = typeof(ICurrencyWebService);
            var types = loader.LoadTypesDerivedFrom(type);
            foreach (var t in types)
            {
                sc.AddTransient(type,t);
            }
        }

    }
}

namespace CurrencyServiceClient
{
    using CommonApiAccessProvider.ApiAccess;
    using CurrencyService.Common.Interfaces;
    using CurrencyService.DAL;
    using CurrencyService.DB;
    using GenericLibraryParser;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            string c1 = "ETH";
            string c2 = "BTC";
            ServiceProvider sp = GetSP();
            var wwss = sp.GetServices<ICurrencyWebService>();
            var data = wwss.AsParallel().SelectMany(x =>x.GetHistoricalTrades<IHistoricalTrade>(c1, c2));
            DbWriteService dws = sp.GetService<DbWriteService>();
            dws.WriteHistoricalTrades(data);
            DbReadService drs = sp.GetService<DbReadService>();
            IEnumerable<CurrencyService.DB.Models.HistoricalTrade> hTradesFromDatabase = drs.GetHistoricalTrades();
            decimal getMaxPrice = drs.GetHistoricalTrades().Max(x => x.Price);
        }


        public static ServiceProvider GetSP()
        {
            var sp=
             new ServiceCollection()
                .AddTransientFromLibrary<ICurrencyWebService>(Directory.GetCurrentDirectory() + "\\CurrencyServices")
                .AddTransient<IApiAccessProvider, WebRequestBasedApiAccess>()
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .AddSingleton<DbWriteService>()
                .AddSingleton<DbReadService>()
                .BuildServiceProvider();
            return sp;
        }





    }
}

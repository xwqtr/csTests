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
    using CurrencyService.DB.Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
           
            ServiceProvider sp = GetSP();
            DbReadService drs = sp.GetService<DbReadService>();
            IEnumerable<HistoricalTrade> hTradesFromDatabase = drs.GetHistoricalTrades();
            decimal getMaxPrice = drs.GetHistoricalTrades().Max(x => x.Price);
        }


        public static ServiceProvider GetSP()
        {
            var sp=
             new ServiceCollection()
                .AddSingleton<DbReadService>()
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .BuildServiceProvider();
            return sp;
        }





    }
}

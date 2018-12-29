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
    using System;
    using System.Threading;

    public class Program
    {
        private static Timer _timer;


        public ServiceProvider _sp;
        public DbReadService _drs;
        public Program()
        {
            _sp = GetSP();
            _drs = _sp.GetService<DbReadService>();
            _timer = new Timer(BGJOB, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
            
        }
        public static void Main(string[] args)
        {
            new Program();
            Thread.Sleep(Timeout.Infinite);
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

        public void BGJOB(object state) {
            var getMaxPrice = _drs.GetHistoricalTrades().GroupBy(x => x.CurrencyName).Select(x => new {
                x.Key,
                MaxPrice = x.Max(z => z.Price),
                AveragePrice = x.Average(z=>z.Price)
            });
            foreach (var p in getMaxPrice)
            {
                Console.WriteLine($"Hey! MaxPrice For{p.Key} is {p.MaxPrice}, AveragePrice is  {p.AveragePrice}");
            }
            
        }




    }
}

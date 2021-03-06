﻿namespace CurrencyServiceClient
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
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;
    using Microsoft.Extensions.Logging.Abstractions;
    using Sentry.Protocol;

    public class Program
    {
        private Timer _timer;
        private static ServiceProvider _sp;
        private readonly DbReadService _drs;
        private readonly ILogger<Program> _logger;
        public Program(DbReadService drs, ILogger<Program> logger)
        {
            _logger = logger;
            _drs = drs;
            
            
        }
        public static void Main(string[] args)
        {
            _sp = GetSP();
            var p =  _sp.GetService<Program>();
            p.Run();
            Thread.Sleep(Timeout.Infinite);
            
           
        }

        public void Run()
        {
            _timer = new Timer(BGJOB, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        }

        public static ServiceProvider GetSP()
        {
            var sp =
             new ServiceCollection()
                .AddLogging(x => {
                    x.AddConsole();
                    x.AddSentry(z => { z.Dsn = "https://6c8eb9aa732e4eefbbf2203893346e9c:31cb8dde16394c0ea0a70937364aa6f9@sentry.io/1361808";
                        z.DiagnosticsLevel = SentryLevel.Info;
                    }
                );
                })
                .AddSingleton<Program>()
                .AddSingleton<DbReadService>()
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .BuildServiceProvider();
            return sp;
        }

        public void BGJOB(object state)
        {

            try
            {
                var getMaxPrice = _drs
                    .GetHistoricalTrades()
                    .GroupBy(x => x.CurrencyName)
                    .Select(x => new
                {
                    x.Key,
                    MaxPrice = x.Max(z => z.Price),
                    AveragePrice = x.Average(z => z.Price)
                });
                foreach (var p in getMaxPrice)
                {
                    Console.WriteLine($"Hey! MaxPrice For{p.Key} is {p.MaxPrice}, AveragePrice is  {p.AveragePrice}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e,e.Message);
            }


        }




    }
}

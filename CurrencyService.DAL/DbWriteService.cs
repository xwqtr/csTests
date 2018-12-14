namespace CurrencyService.DAL
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using CurrencyService.Common.Interfaces;
    using CurrencyService.DB;
    using CurrencyService.DB.Models;
    using Microsoft.EntityFrameworkCore.Internal;

    public class DbWriteService
    {

        private readonly CurrencyDbContext _currencyDbContext; 
        public DbWriteService(CurrencyDbContext currencyDbContext) {
            _currencyDbContext = currencyDbContext;
            
        }

        public void WriteHistoricalTrades<T>(IEnumerable<T> historicalTrades) where T : IHistoricalTrade
        {
            var minTime = historicalTrades.Min(x => x.Time);
            var hts = historicalTrades
                .Where(z => !_currencyDbContext.HistoricalTrades.Where(c=>c.Time>= minTime).Any(x => x.Equals(z)))
                .Select(x => new HistoricalTrade()
                {
                    Count = x.Count,
                    Time = x.Time,
                    IsBought = x.IsBought,
                    Price = x.Price,
                    CurrencyName = x.CurrencyName,
                    CurrencyToConvertName = x.CurrencyToConvertName
            });
            _currencyDbContext.HistoricalTrades.AddRange(hts);
            _currencyDbContext.SaveChanges();
        }

       
    }
}

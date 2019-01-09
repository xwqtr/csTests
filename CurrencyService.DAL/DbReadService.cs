namespace CurrencyService.DAL
{
    using CurrencyService.DB;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Linq.Expressions;
    using CurrencyService.Common.Interfaces;
    using CurrencyService.DB.Models;
    using Microsoft.Extensions.Logging;
    

    public class DbReadService
    {
        private readonly CurrencyDbContext _currencyDbContext;
        public DbReadService(CurrencyDbContext currencyDbContext, ILogger<DbReadService> logger)
        {
            _currencyDbContext = currencyDbContext;
            logger.LogInformation($"WOW {nameof(DbReadService)} created!");
        
        }


        public IEnumerable<HistoricalTrade> GetHistoricalTrades(Expression<Func<HistoricalTrade, bool>> expression = null)  {

            if (expression == null) {
                expression = x => true;
            }
           return _currencyDbContext.HistoricalTrades.Where(expression);
        }

    }
}

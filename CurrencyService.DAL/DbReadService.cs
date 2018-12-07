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

    public class DbReadService
    {
        private readonly CurrencyDbContext _currencyDbContext;
        public DbReadService(CurrencyDbContext currencyDbContext)
        {
            _currencyDbContext = currencyDbContext;

        }


        public IEnumerable<HistoricalTrade> GetHistoricalTrades(Expression<Func<HistoricalTrade, bool>> expression = null)  {

            if (expression == null) {
                expression = x => true;
            }
           return _currencyDbContext.HistoricalTrades.Where(expression);
        }

    }
}

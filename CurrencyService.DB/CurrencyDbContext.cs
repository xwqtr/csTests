namespace CurrencyService.DB
{
    using CurrencyService.DB.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<HistoricalTrade> HistoricalTrades { get; set; }
    }
}

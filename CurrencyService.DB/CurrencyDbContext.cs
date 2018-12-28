namespace CurrencyService.DB
{
    using CurrencyService.DB.Models;
    using Microsoft.EntityFrameworkCore;

    using Microsoft.EntityFrameworkCore.SqlServer;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Logging;

    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions options, ILogger<CurrencyDbContext> logger) : base(options)
        {
            try
            {
                this.Database.EnsureCreated();
            }
            catch (Exception e)
            {
                logger.LogError($"MESSAGE:{e.Message} STACKTRACE:{e.StackTrace}");
            }
            
        }
        public DbSet<HistoricalTrade> HistoricalTrades { get; set; }
    }
}

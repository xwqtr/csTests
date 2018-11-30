namespace CurrencyService.DB
{
    using CurrencyService.DB.Models;
    using Microsoft.EntityFrameworkCore;

    using Microsoft.EntityFrameworkCore.SqlServer;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CurrencyDbContext : DbContext
    {
        public CurrencyDbContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
        }
        public DbSet<HistoricalTrade> HistoricalTrades { get; set; }
    }
}

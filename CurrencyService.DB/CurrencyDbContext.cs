namespace CurrencyService.DB
{
    using CurrencyService.DB.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using System;

    public class CurrencyDbContext : IdentityDbContext<ApplicationUser,Role,Guid,UserClaim,UserRole,UserLogin,RoleClaim,UserToken>
    {
        public CurrencyDbContext(DbContextOptions options, ILogger<CurrencyDbContext> logger) : base(options)
        {
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception e)
            {
                logger.LogError($"MESSAGE:{e.Message} STACKTRACE:{e.StackTrace}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<HistoricalTrade> HistoricalTrades { get; set; }

        public DbSet<BgServiceConfiguration> BgServiceConfigurations { get; set; }
    }


}

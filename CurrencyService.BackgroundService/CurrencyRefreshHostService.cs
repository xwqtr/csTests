namespace CurrencyService.BackgroundService
{
    using CurrencyService.Common.Interfaces;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using System.ServiceProcess;
    using System;
    using CurrencyService.DAL;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting.WindowsServices;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    using System.Threading;
    using System.Threading.Tasks;

    public class CurrencyRefreshHostService : BackgroundService
    {

        private readonly IEnumerable<ICurrencyWebService> _currencyWebServices;
        private readonly DbWriteService _dbWrite;
        public CurrencyRefreshHostService(IHost host) 
        {
            _currencyWebServices = host.Services.GetServices<ICurrencyWebService>();
            _dbWrite = host.Services.GetService<DbWriteService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine($"{nameof(CurrencyRefreshHostService)} is starting.");

            stoppingToken.Register(() => Console.WriteLine($"{nameof(CurrencyRefreshHostService)} is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"{nameof(CurrencyRefreshHostService)} is doing background work.");

                await RefreshDbData("ETH", "BTC"); //configure from M.ext.configurations
            }
        }

        private async Task RefreshDbData(string currency1, string currencyToCompare) {
            var data = _currencyWebServices.AsParallel().SelectMany(x => x.GetHistoricalTrades<IHistoricalTrade>(currency1, currencyToCompare));
            _dbWrite.WriteHistoricalTrades(data);
        }

        
    }
}

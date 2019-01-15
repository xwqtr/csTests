﻿namespace CurrencyService.BackgroundService
{
    using CurrencyService.Common.Interfaces;
    using CurrencyService.DAL;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceProcess;
    using System.Threading;
    using System.Threading.Tasks;
    public class CurrencyRefreshHostService : IHostedService, IDisposable
    {
        private readonly IEnumerable<ICurrencyWebService> _currencyWebServices;
        private readonly DbWriteService _dbWrite;
        private readonly ILogger _logger;
        private Timer _timer;
        private AutoResetEvent _autoEvent;
        public CurrencyRefreshHostService(IServiceProvider sp, ILogger<CurrencyRefreshHostService> logger)
        {
            
            _currencyWebServices = sp.GetServices<ICurrencyWebService>();
            _dbWrite = sp.GetService<DbWriteService>();
            _logger = logger;
            _autoEvent = new AutoResetEvent(true);
        }

        public async void DoWork(object state)
        {
            ((AutoResetEvent)state).WaitOne();
            _logger.LogInformation($"Thread:{Thread.CurrentThread.ManagedThreadId} started DoWork");
            _logger.LogInformation("Timed Background Service is working.");
            await RefreshDbData("ETH", "BTC");
            ((AutoResetEvent)state).Set();
            
            
            
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is starting.");
            _timer = new Timer(DoWork, _autoEvent, TimeSpan.Zero, TimeSpan.FromMilliseconds(200));
            return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Timed Background Service is stopping.");
             _timer?.Change(Timeout.Infinite, 0);
             return Task.CompletedTask;

        }

        private async Task RefreshDbData(string currency1, string currencyToCompare)
        {
            var data = (await Task.WhenAll(_currencyWebServices.Select(x =>x.GetHistoricalTrades<IHistoricalTrade>(currency1, currencyToCompare)))).SelectMany(x=>x);
            _dbWrite.WriteHistoricalTrades(data);
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}

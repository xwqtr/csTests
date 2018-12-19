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

    public class CurrencyRefreshHostService : WebHostService
    {

        private readonly IEnumerable<ICurrencyWebService> _currencyWebServices;
        private readonly DbWriteService _dbWrite;
        public CurrencyRefreshHostService(IWebHost host) : base(host)
        {
            _currencyWebServices = host.Services.GetServices<ICurrencyWebService>();
            _dbWrite = host.Services.GetService<DbWriteService>();
        }
        public void RefreshDbData(string currency1, string currencyToCompare) {

            var data = _currencyWebServices.AsParallel().SelectMany(x => x.GetHistoricalTrades<IHistoricalTrade>(currency1, currencyToCompare));
            _dbWrite.WriteHistoricalTrades(data);

        } 
    }
}

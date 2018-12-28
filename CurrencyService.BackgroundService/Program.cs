using CommonApiAccessProvider.ApiAccess;
using CurrencyService.Common.Interfaces;
using CurrencyService.DAL;
using CurrencyService.DB;
using GenericLibraryParser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using System.Reflection;

namespace CurrencyService.BackgroundService
{
    
    public class Program
    {

        public async static Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));
            var builder = new HostBuilder().ConfigureServices((hc, services) =>
            {
               services.AddTransientFromLibrary<ICurrencyWebService>(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CurrencyServices")
              .AddTransient<IApiAccessProvider, WebRequestBasedApiAccess>()
              .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=V-ILSEKA\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
              .AddSingleton<DbWriteService>()
              .AddHostedService<CurrencyRefreshHostService>()
              .AddLogging();
            })
            .ConfigureLogging(x=> {
                x.AddConsole();
                x.AddEventLog();
            });

            if (isService)
            {
                await builder.RunAsServiceAsync();
            }
            else
            {
                await builder.RunConsoleAsync();
            }

        }
    }
}

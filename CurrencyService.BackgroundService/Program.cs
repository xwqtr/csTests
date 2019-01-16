using CommonApiAccessProvider.ApiAccess;
using CurrencyService.Common.Interfaces;
using CurrencyService.DAL;
using CurrencyService.DB;
using GenericLibraryParser;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace CurrencyService.BackgroundService
{

    public class Program
    {

        public static async Task Main(string[] args)
        {
            bool isService = !(Debugger.IsAttached || args.Contains("--console"));
            IHostBuilder builder = new HostBuilder().ConfigureServices((hc, services) =>
            {
                services.AddTransientFromLibrary<ICurrencyWebService>(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\CurrencyServices")
               .AddTransient<IApiAccessProvider, WebRequestBasedApiAccess>()
               .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=V-ILSEKA\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
               .AddSingleton<DbWriteService>()
               .AddHostedService<CurrencyRefreshHostService>()
               .AddLogging();
            })
            .ConfigureAppConfiguration(x=>x.AddJsonFile("config.json",false,reloadOnChange:true))
            .ConfigureLogging(x =>
            {
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

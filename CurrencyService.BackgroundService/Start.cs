namespace CurrencyService.BackgroundService
{
    using CommonApiAccessProvider.ApiAccess;
    using CurrencyService.Common.Interfaces;
    using CurrencyService.DAL;
    using CurrencyService.DB;
    using GenericLibraryParser;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.WindowsServices;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    class Start
    {
        static void Main(string[] args)
        {
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            if (Debugger.IsAttached || args.Contains("console"))
            {
                Debugger.Launch();
                var whb = new WebHostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureServices(x => GetSP())
                    .Build();
                whb.Run();
            }
            else
            {
                var whb = new WebHostBuilder().UseContentRoot(pathToContentRoot).ConfigureServices(x => GetSP()).Build();
                whb.RunAsService();
            }
            
            
        }


        public static ServiceProvider GetSP()
        {

           
            var sp =
             new ServiceCollection()
                .AddTransientFromLibrary<ICurrencyWebService>(Directory.GetCurrentDirectory() + "\\CurrencyServices")
                .AddTransient<IApiAccessProvider, WebRequestBasedApiAccess>()
                .AddDbContext<CurrencyDbContext>(x => x.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Currencies;Trusted_Connection=True;"))
                .AddSingleton<DbWriteService>()
                .BuildServiceProvider();
            return sp;
        }
    }
}

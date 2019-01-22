namespace CurrencyService.WebApi.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using CurrencyService.DAL;
    using CurrencyService.WebApi.Models.Chart;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    [Authorize]
    [Route("[controller]/[Action]")]
    public class ChartController : Controller
    {
        private readonly DbReadService _drs;
        public ChartController(DbReadService drs)
        {
            _drs = drs;
        }
        public IActionResult GetChartDataByYear(string currencyName,int fromYear)
        {
            var data = _drs.GetHistoricalTrades(z=>z.CurrencyName == currencyName && z.IsBought);
            var model = data
                .Select(x => new { DateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(x.Time), Cost = x.Price, Name = x.CurrencyName })
                .GroupBy(x => new { x.DateTime.Year,x.DateTime.Month  }).Where(z => z.Key.Year >= fromYear).OrderBy(x=>x.Key.Month)
                .Select(z=> new ChartData() {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(z.Key.Month),
                    Cost = z.Average(x=>x.Cost),
                    Name = z.FirstOrDefault().Name
                }).OrderBy(x => x.Month);
            return new JsonResult(model);
        }

        public IActionResult GetChartDataByDay(string currencyName, int fromDay)
        {
            var data = _drs.GetHistoricalTrades(z => z.CurrencyName == currencyName && z.IsBought);
            var model = data
                .Select(x => new { DateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(x.Time), Cost = x.Price, Name = x.CurrencyName })
                .GroupBy(x => new {  x.DateTime.Month, x.DateTime.Day }).Where(z => z.Key.Day >= fromDay).OrderBy(x => x.Key.Day)
                .Select(z => new ChartData()
                {
                    Month = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(z.Key.Month)+"/"+z.Key.Day,
                    Cost = z.Average(x => x.Cost),
                    Name = z.FirstOrDefault().Name
                });
            return new JsonResult(model);
        }
    }
}

namespace CurrencyService.WebApi.Controllers
{
    using CurrencyService.Common.Interfaces;
    using CurrencyService.DAL;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CurrenciesController : ControllerBase
    {
        private readonly DbReadService _drs;
        public CurrenciesController(DbReadService drs)
        {
            _drs = drs;
        }
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<IHistoricalTrade>> Get()
        {
            return new JsonResult(_drs.GetHistoricalTrades().Take(1000));
        }
    }
}

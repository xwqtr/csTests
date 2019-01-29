using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyService.DB;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using CurrencyService.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNet.OData.Routing;
using CurrencyService.Common.Interfaces;
using CurrencyService.DAL;

namespace CurrencyService.WebApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    public class HistoricalTradeController : ODataController
    {

        private readonly DbReadService _service;
        public HistoricalTradeController(DbReadService service) {
            _service = service;
        }
        [EnableQuery]
        [HttpGet]
        public IQueryable<IHistoricalTrade> Get()  { 
        return _service.GetHistoricalTrades().AsQueryable();
        }
        
    }
}
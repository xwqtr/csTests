using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileSysIO = System.IO.File;
using System.Threading.Tasks;
using CurrencyService.WebApi.Models.BackgroundWorker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceHelper;
using Newtonsoft.Json;
using CurrencyService.BackgroundService.Common;
using System.Diagnostics;
using CurrencyService.DAL;

namespace CurrencyService.WebApi.Controllers
{
    [Route("[controller]/[Action]")]
    [Authorize]
    public class BackgroundWorkerController : Controller
    {
        private readonly string _backgroundServicePathFolder;
        private const string _bgServiceName = "CurrencyService.BackgroundService";
        private readonly DbWriteService _dbWriteService; 
        public BackgroundWorkerController(DbWriteService dbWriteService) {
            _backgroundServicePathFolder = Path.GetDirectoryName(Finder.GetServicePath(_bgServiceName));
            _dbWriteService = dbWriteService;
        }
        public IActionResult Index()
        {
            return View(new BackgroundWorkerConfigurationViewModel());
        }

        [HttpPost]
        public IActionResult SaveConfig(BackgroundWorkerConfigurationViewModel model) {

            var result = new SaveConfigResult();

            try
            {
                _dbWriteService.AddBgWorkerConfiguration(model);
                result.Successfull = true;
            }
            catch(Exception e)
            {
                result.Successfull = false;
                result.Message = e.Message;
            }
                   
                
                
            
            return View(result);
        }

        
    }
}
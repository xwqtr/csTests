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

namespace CurrencyService.WebApi.Controllers
{
    [Route("[controller]/[Action]")]
    [Authorize]
    public class BackgroundWorkerController : Controller
    {
        private readonly string _backgroundServicePathFolder;
        private const string _bgServiceName = "CurrencyService.BackgroundService";
        public BackgroundWorkerController() {
            _backgroundServicePathFolder = Path.GetDirectoryName(Finder.GetServicePath(_bgServiceName));
        }
        public IActionResult Index()
        {
            return View(new BackgroundWorkerConfigurationViewModel());
        }

        [HttpPost]
        public IActionResult SaveConfig(BackgroundWorkerConfigurationViewModel model) {

            var result = new SaveConfigResult();
            var jsonConfigName = Path.Combine(_backgroundServicePathFolder, "config.json");
            if (FileSysIO.Exists(jsonConfigName))
            {
                var content = JsonConvert.DeserializeObject<BackgroundWorkerConfiguration>(FileSysIO.ReadAllText(jsonConfigName));
                content.SecondsInterval = model.SecondsInterval;
                var newConfigText = JsonConvert.SerializeObject(content);
                FileSysIO.WriteAllText(jsonConfigName, newConfigText);
                try
                {
                    Manager.RestartService(_backgroundServicePathFolder,_bgServiceName);
                    result.Successfull = true;
                }
                catch(Exception e)
                {
                    result.Successfull = false;
                    result.Message = $"Cannot restartService {e.Message}";
                }
                   
                
                
            }
            else {
                result.Successfull = false;
                result.Message= $"File {jsonConfigName} not found";
            }
            return View(result);
        }

        
    }
}
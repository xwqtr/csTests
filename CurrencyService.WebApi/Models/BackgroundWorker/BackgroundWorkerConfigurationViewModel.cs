using CurrencyService.BackgroundService.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.WebApi.Models.BackgroundWorker
{
    public class BackgroundWorkerConfigurationViewModel : IBackgroundWorkerConfiguration
    {
        public int SecondsInterval { get; set; }
    }
}

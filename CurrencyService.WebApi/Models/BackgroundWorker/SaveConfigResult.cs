using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyService.WebApi.Models.BackgroundWorker
{
    public class SaveConfigResult
    {
        public bool Successfull { get; set; }

        public string Message { get; set; }
    }
}

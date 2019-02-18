using CurrencyService.BackgroundService.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyService.DB.Models
{
    public class BgServiceConfiguration : IBackgroundWorkerConfiguration
    {
        public Guid Id { get; set; }

        public int SecondsInterval { get; set; }

        public DateTime ChangeDate { get; set; }
    }
}

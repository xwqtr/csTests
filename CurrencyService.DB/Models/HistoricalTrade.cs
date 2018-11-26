

namespace CurrencyService.DB.Models
{
    using CurrencyService.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class HistoricalTrade : IHistoricalTrade
    {
        [Key]
        public Guid Id { get; set; }
        public string Price { get; set; }
        public long Time { get; set; }
        public decimal Count { get; set; }
        public bool IsBought { get; set; }
    }
}



namespace CurrencyService.DB.Models
{
    using CurrencyService.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    public class HistoricalTrade : IHistoricalTrade, IEquatable<HistoricalTrade>
    {
        [Key]
        public Guid Id { get; set; }
        public string Price { get; set; }
        public long Time { get; set; }
        public decimal Count { get; set; }
        public bool IsBought { get; set; }

        public bool Equals(IHistoricalTrade other)
        {
            return this.Price == other.Price && this.Time == other.Time && this.Count == other.Count && this.IsBought == other.IsBought;
        }

        public bool Equals(HistoricalTrade other)
        {
          return   this.Id == other.Id || Equals(other);
        }
    }
}



namespace CurrencyService.DB.Models
{
    using CurrencyService.Common.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text;
    public class HistoricalTrade : IHistoricalTrade, IEquatable<HistoricalTrade>
    {
        [Key]
        public Guid Id { get; set; }
        [Column(TypeName = "decimal(18,8)")]
        public decimal Price { get; set; }
        public long Time { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Count { get; set; }
        public bool IsBought { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyToConvertName { get; set; }
        public bool Equals(IHistoricalTrade other)
        {
            return this.Time == other.Time && this.Count == other.Count && this.Price == other.Price  && this.IsBought == other.IsBought && this.CurrencyName==other.CurrencyName&& this.CurrencyToConvertName==this.CurrencyToConvertName;
        }

        public bool Equals(HistoricalTrade other)
        {
          return   this.Id == other.Id && Equals(other);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister.Models
{
    public class Currency
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal ConversionRate { get; set; }

        public Currency( string currencyCode, string currencyName, decimal conversionRate)
        {
            CurrencyCode = currencyCode;
            CurrencyName = currencyName;
            ConversionRate = conversionRate;
        }
    }
}

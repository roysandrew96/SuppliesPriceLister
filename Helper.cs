using Microsoft.Extensions.Configuration;
using SuppliesPriceLister.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister
{
    static class Helper
    {
        public static decimal GetUSDExchangeRate()
        {
            //
            // A default USD to AUD Exchange rate, should one not be found in the
            // Configuration file
            //
            const decimal defaultUSDExchangeRate = 0.75m;
            string exchangeRateString = string.Empty;
            decimal usdExchangeRate = 0;

            try
            {
                var builder = new ConfigurationBuilder()
                    .AddJsonFile($"appsettings.json", true, true);
                var config = builder.Build();
                exchangeRateString = config["audUsdExchangeRate"];
                if (!decimal.TryParse(exchangeRateString, out usdExchangeRate))
                {
                    usdExchangeRate = defaultUSDExchangeRate;
                }
            }
            catch
            {
                // Should we not be able to able to successfully retrieve the
                // USD Exchange Rate, we'll note that, and use a default value 
                usdExchangeRate = defaultUSDExchangeRate;
            }
            return usdExchangeRate;
        }

        public static Dictionary<string, Currency> InitialiseCurrencies( decimal usdExchangeRate )
        {
            Dictionary<string, Currency> currencies = new Dictionary<string, Currency>();
            Currency aud = new Currency("AUD", "Australian $", 1.0m);
            currencies.Add(aud.CurrencyCode, aud);
            Currency usd = new Currency("USD", "United States $", usdExchangeRate);
            currencies.Add(usd.CurrencyCode, usd);
            return currencies;
        }

    }
}

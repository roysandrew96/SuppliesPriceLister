using Microsoft.Extensions.Configuration;
using SuppliesPriceLister.Models;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace SuppliesPriceLister
{
    public class Helper
    {
        decimal usdExchangeRate = 0m;

        public Dictionary<string, Currency> Currencies
        {
            get;
        }

        public decimal USDExchangeRate
        {
            get
            {
                return usdExchangeRate;
            }
        }

        public Helper()
        {
            // Retrieve the USD Exchange Rate from the Configuration File
            usdExchangeRate = GetUSDExchangeRate();
            // Instantiate the Currencies Dictionary
            Currencies = new Dictionary<string, Currency>();
            InitialiseCurrencies(usdExchangeRate);
        }

        public decimal GetUSDExchangeRate()
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

        /// <summary>
        /// Initialises the Currencies Collection with the two Currencies we are interested in AUD and USD
        /// </summary>
        /// <param name="usdExchangeRate"></param>
        public void InitialiseCurrencies( decimal usdExchangeRate )
        {
            Currency aud = new Currency("AUD", "Australian $", 1.0m);
            Currencies.Add(aud.CurrencyCode, aud);
            Currency usd = new Currency("USD", "United States $", usdExchangeRate);
            Currencies.Add(usd.CurrencyCode, usd);
        }

        /// <summary>
        /// The DisplayProducts method displays a List of Products as per an agreed format, in descending AUD Price order
        /// </summary>
        /// <param name="products"></param>
        public void DisplayProducts( List<Product> products )
        {
            // Firstly, sort the products list appropriately
            var orderedProducts = from s in products
                                          orderby s.AUDPrice descending
                                          select s;
            // Display a very simple heading - note the output has not be column aligned
            Console.WriteLine("Product Id, Description, Price (AUD)");
            Console.WriteLine();
            // Iterate over the sorted product list and display the product details, with a blank line after each
            foreach (Product p in orderedProducts)
            {
                Console.WriteLine($"{p.ProductId}, {p.Description}, {p.AUDPrice:C}");
                Console.WriteLine();
            }
        }

    }
}

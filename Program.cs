using System;
using System.Collections.Generic;
using System.Linq;
using SuppliesPriceLister.Models;

namespace SuppliesPriceLister
{
    static class Program
    {
        static void Main()
        {
            // Variable Declarations
            decimal usdExchangeRate = 0;
            Dictionary<string, Currency> Currencies = new Dictionary<string, Currency>();
            List<Product> Products = new List<Product>();

            // Retrieve the USD Exchange Rate from the Configuration File
            usdExchangeRate = Helper.GetUSDExchangeRate();

            // Set up a collection of currencies to use
            Currencies = Helper.InitialiseCurrencies( usdExchangeRate );

            // Load Humphries CSV Data

            List<Product> humphriesProducts = DataLoad.LoadCSVProducts("humphries.csv");

            // Load MegaCorp JSON Data

            List<Product> megaCorpProducts = DataLoad.LoadJSONProducts("megacorp.json");

            // Merge the lists

            List<Product> allProducts = humphriesProducts.Union(megaCorpProducts).ToList();

            // Display the merged list of products

            Helper.DisplayProducts(allProducts);

            Console.WriteLine("Coding Challenge Output Complete");
            Console.WriteLine("================================");
            Console.ReadLine();
        }

    }
}

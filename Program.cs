using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
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

            List<Product> HumphriesProducts = DataLoad.LoadCSVProducts("humphries.csv");

            // Load MegaCorp JSON Data

            List<mcorpPartner> mcorpPartners = DataLoad.LoadJSONProducts("megacorp.json");

            Console.WriteLine($"Exchange Rate Decimal {usdExchangeRate}");
            Console.ReadLine();
        }

    }
}

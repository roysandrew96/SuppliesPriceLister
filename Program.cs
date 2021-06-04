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
            // Create the Helper Class, which manages the Exchange Rate and Currencies
            Helper helper = new Helper();

            // Load Humphries CSV Data

            List<Product> humphriesProducts = DataLoad.LoadCSVProducts("Humphries", "humphries.csv", helper);

            // Load MegaCorp JSON Data

            List<Product> megaCorpProducts = DataLoad.LoadJSONProducts("MegaCorp", "megacorp.json", helper);

            // Merge the lists

            List<Product> allProducts = humphriesProducts.Union(megaCorpProducts).ToList();

            // Display the merged list of products

            helper.DisplayProducts(allProducts);

            Console.WriteLine("Coding Challenge Output Complete");
            Console.WriteLine("================================");
            Console.ReadLine();
        }

    }
}

using SuppliesPriceLister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace SuppliesPriceLister
{
    static class DataLoad
    {
        public static List<Product> LoadCSVProducts( string fileName)
        {
            List<Product> products = new List<Product>();
            var dataRows = File.ReadLines(fileName);
            int iRowNumber = 0;
            // Process each row in the CSV File
            // NOTE - I'm not convinced this is the MOST EFFICIENT way this could be done ... In fact I'm almost
            // certain that more efficient ways could be found
            foreach (string dataRow in dataRows)
            {
                // Firstly, skip the heading row
                if (iRowNumber++ > 0)
                {
                    string[] values = dataRow.Split(",");
                    string productId = values[0];
                    string productDescription = values[1];
                    string currencyCode = "AUD";
                    string localPriceString = values[3];
                    decimal localPrice = 0.0m;
                    if (!decimal.TryParse(localPriceString,out localPrice))
                    {
                        localPrice = 0.0m;
                    }
                    Product prod = new Product( values[0], values[1], currencyCode, localPrice);
                    products.Add(prod);
                }
            }
            return products;
        }
        /// <summary>
        /// Load the Humphries Products which are in a JSON format
        /// </summary>
        /// <param name="fileName">The file path</param>
        /// <returns></returns>
        public static List<Product> LoadJSONProducts( string fileName)
        {
            List<Product> products = new List<Product>();

            var jsonString = File.ReadAllText(fileName);
            var partners = JsonConvert.DeserializeObject<mcorpPartners>(jsonString);

            foreach (mcorpPartner partner in partners.Partners)
            {
                foreach ( mcorpSupply supply in partner.Supplies)
                {
                    Product p = new Product(supply.Id, supply.Description, "USD", (decimal)supply.PriceInCents / 100);
                    products.Add(p);
                }
            }

            return products;
        }
    }
}

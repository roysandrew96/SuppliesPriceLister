using SuppliesPriceLister.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

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

        public static List<mcorpPartner> LoadJSONProducts( string fileName)
        {
            List<mcorpPartner> partners;

            string jsonString = File.ReadAllText(fileName);
            partners = JsonSerializer.Deserialize<List<mcorpPartner>>(jsonString);
            return partners;
        }
    }
}

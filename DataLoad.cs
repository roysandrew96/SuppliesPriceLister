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
        public static List<Product> LoadCSVProducts( string supplierCode, string fileName, Helper helperReference)
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
                    Product prod = new Product( supplierCode, values[0], values[1], currencyCode, localPrice, helperReference);
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
        public static List<Product> LoadJSONProducts(string supplierCode, string fileName, Helper helperReference)
        {
            List<Product> products = new List<Product>();
            // Read the text of the JSON file, and use the NewtonSoft JSONConvert Class to Deserialize the to a Class Hierarchy
            // determined by reviewing the JSON file structure
            var jsonString = File.ReadAllText(fileName);
            var partners = JsonConvert.DeserializeObject<mcorpPartners>(jsonString);

            // Iterate over the classes to add the underlying Megacorp supplies as products to a List of Products
            foreach (mcorpPartner partner in partners.Partners)
            {
                foreach ( mcorpSupply supply in partner.Supplies)
                {
                    //
                    // Note that as the Humphries Products prices are expressed as cents, we divide by 100
                    //
                    Product p = new Product(supplierCode, supply.Id, supply.Description, "USD", (decimal)supply.PriceInCents / 100, helperReference);
                    products.Add(p);
                }
            }
            return products;
        }
    }
}

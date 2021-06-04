using System;

namespace SuppliesPriceLister.Models
{
    /// <summary>
    /// The Product Class is the primary class that represents a unified view Products
    /// </summary>
    public class Product
    {
        decimal localPrice;
        Helper helper;

        public Product(Helper helperReference)
        {
            SupplierCode = "";
            ProductId = "";
            Description = "";
            localPrice = 0.0m;
            SupplierCurrencyCode = "AUD";
            helper = helperReference;
        }

        public Product( string supplierCode, string productId, string description, string supplierCurrencyCode, decimal localPrice, Helper helperReference )
        {
            SupplierCode = supplierCode;
            ProductId = productId;
            Description = description;
            LocalPrice = localPrice;
            SupplierCurrencyCode = supplierCurrencyCode;
            helper = helperReference;
        }

        public String SupplierCode { get; set; }
        
        public String SupplierCurrencyCode { get; set; }

        public string ProductId { get; set; }

        public string Description { get; set; }

        public decimal LocalPrice
        {
            get { return localPrice;  }
            set { localPrice = value; }
        }

        public decimal AUDPrice
        {
            get
            {
                // Using the Currency the Product's price is expressed in, determine the AUD price.
                Currency currency;
                var currencies = helper.Currencies;
                // Ensure the Supplier's defined Currency Code is one we know about ...
                if (currencies.ContainsKey(SupplierCurrencyCode))
                {
                    currency = currencies[SupplierCurrencyCode];
                }
                else
                {
                    // If not, default to AUD
                    currency = currencies["AUD"];
                };
                // If the Conversion Rate were 0 for whatever reason, simply return the LocalPrice
                if (currency.ConversionRate == 0.0m)
                {
                    return LocalPrice;
                }
                else
                {
                    return LocalPrice / currency.ConversionRate;
                }
            }
        }
    }
}

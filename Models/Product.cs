using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister.Models
{

    class Product
    {
        decimal localPrice;

        public Product()
        {
            ProductId = "";
            Description = "";
            localPrice = 0.0m;
            SupplierCurrencyCode = "AUD";
            
        }

        public Product( string productId, string description, string supplierCurrencyCode, decimal localPrice )
        {
            ProductId = productId;
            Description = description;
            LocalPrice = localPrice;
            SupplierCurrencyCode = supplierCurrencyCode;
        }

        public String SupplierCurrencyCode { get; set; }

        public string ProductId { get; set; }

        public string Description { get; set; }

        public decimal LocalPrice
        {
            get { return localPrice;  }
            set { localPrice = value; }
        }

        public decimal AUDPrice { get; set; }
    }
}

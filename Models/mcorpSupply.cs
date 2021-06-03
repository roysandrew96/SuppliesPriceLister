using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister.Models
{
    class mcorpSupply
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string UOM { get; set; }
        public int PriceInCents { get; set; }
        public string ProviderId {get; set;}
        public string MaterialType { get; set; }
    }
}

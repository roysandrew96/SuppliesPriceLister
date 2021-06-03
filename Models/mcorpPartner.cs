using System;
using System.Collections.Generic;
using System.Text;

namespace SuppliesPriceLister.Models
{
    class mcorpPartner
    {
        public string Name { get; set; }
        public string PartnerType { get; set; }
        public string PartnerAddress { get; set; }
        public mcorpSupply[] Supplies { get; set; }

    }
}

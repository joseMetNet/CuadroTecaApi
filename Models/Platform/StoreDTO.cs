using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Platform
{
    public class StoreResponse
    {
        public int idStore { get; set; }
        public string NameStore { get; set; }
        public string country { get; set; }
        public decimal priceProduct { get; set; }
        public decimal PriceShipping { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Product
{
    public class ProductResponse
    {
        public int idProduct { get; set; }
        public string NameProduct { get; set; }
        public string dateAdd { get; set; }
    }
    public class ProductResponseCategory
    {
        public int idCategory { get; set; }
        public string NameCategory { get; set; }
    }
}

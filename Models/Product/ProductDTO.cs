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
    public class StatusRequride
    {
        public int Status { get; set; }
        public int IdMercadoPag { get; set; }
    }
    public class ProductResponseCategory
    {
        public int idCategory { get; set; }
        public string NameCategory { get; set; }
    }

    public class ProductOrderRequeride
    {

        public int idShippingUser { get; set; }
        public string idProduct { get; set; }
        public string title { get; set; }
        public string quantity { get; set; }
        public string idcurrency { get; set; }
        public string unitPrice { get; set; }
        public string uuid { get; set; }
        public string cdnUrl { get; set; }
        public string frame { get; set; }
        public string edge { get; set; }
        public string country { get; set; }


    }
    public class DataShippingUser
    {
        public int idOrder { get; set; }
        public string Fullname { get; set; }
        public string email { get; set; }
        public string NameStatus { get; set; }


    }

    public class DataEmailAdmins
    { 
        public string FullName { get; set; }
        public string email { get; set; }

    }
    public class PreferenceItemRequestOwn
    {
        public string Title { get; set; }
        public string Quantity { get; set; }
        public string CurrencyId { get; set; }
        public int UnitPrice { get; set; }

    }


}

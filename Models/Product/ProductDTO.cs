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
        public string Status { get; set; }
        public string IdMercadoPag { get; set; }
    }

    public class StatusResponse
    {
        public int idGiftCard { get; set; }
        public string codeGitfCard { get; set; }


    }
    public class ProductResponseCategory
    {
        public int idCategory { get; set; }
        public string NameCategory { get; set; }
    }

    public class directpaymentResponse
    {
        public string url { get; set; }
    }

    public class OrderResponse
    {
        public int idOrder { get; set; }
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
        public int idGiftCardUsedForPurchase { get; set; }
        public string CodeGiftCardUsedForPurchase { get; set; }
        public int totalPrice { get; set; }
        public int idPromotion { get; set; }
        public string editUrl { get; set; }
        public int balanceTemporary { get; set; }
        public decimal iva { get; set; }
        public int priceShipment { get; set; }
        public string present { get; set; }


    }
    public class ProductOrderRequeride2
    {

        public int idShippingUser { get; set; }
        public string idcurrency { get; set; }
        public string country { get; set; }
        public string idGiftCard { get; set; }
        public int totalPrice { get; set; }
        public int idPromotion { get; set; }
    }
    public class DataShippingUser
    {
        public int idOrder { get; set; }
        public int idShippingUser { get; set; }
        public string Fullname { get; set; }
        public string email { get; set; }
        public string NameStatus { get; set; }
        public string present { get; set; }
    }
    public class DataEmailAddressUser
    {
        public string FullName { get; set; }
        public string email { get; set; }
    }
    public class DataEmailAdmins
    { 
        public string FullName { get; set; }
        public string email { get; set; }

    }

    public class GitfCard
    {
        public string idOrder { get; set; }
    }
    public class PreferenceItemRequestOwn
    {
        public string Title { get; set; }
        public string Quantity { get; set; }
        public string CurrencyId { get; set; }
        public int UnitPrice { get; set; }

    }


}

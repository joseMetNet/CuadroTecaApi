using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.ShippingUser
{
    public class ShippingUserRequeride
    {
        public int idShippingUser { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string typeHome { get; set; }
        public string address { get; set; }
        public int pidCity { get; set; }
        public int pidDepartment { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int idProduct { get; set; }
        public int idGiftCard { get; set; }
        public string Nit { get; set; }

    }
    public class ShippingUserGiftCardRequeride
    {
        public int idShippingUserGiftCard { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string typeHome { get; set; }
        public string address { get; set; }
        public int idCity { get; set; }
        public int idDepartment { get; set; }
        public string email { get; set; }
        public int idGiftCard { get; set; }

    }
    public class ShippingAddressGiftCardRequeride
    {
        public int idShippingAddressGiftCard { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public int idShippingUserGiftCard { get; set; }


    }
    public class ShippingUserResponse
    {
        public string api_dice { get; set; }
        public int idShippingUser { get; set; }

    }
    public class storeOrUpdateShippingUserGiftCardResponse
    {
        public string api_dice { get; set; }
        public int idShippingUserGiftCard { get; set; }
    }
    public class ShippingAddressGiftCardResponse
    {
        public string api_dice { get; set; }
        public int idShippingAddressGiftCard { get; set; }
    }
    public class ShippingUserResponse2
    {
        public int idShippingUser { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string typeHome { get; set; }
        public string address { get; set; }
        public int idCity { get; set; }
        public string City { get; set; }
        public int idDepartment { get; set; }
        public string nameDepartment { get; set; }
        public string phone { get; set; }
        public string datetime { get; set; }
        public int idProduct { get; set; }
        public string email { get; set; }
        public string Nit { get; set; }


    }
    public class ShippingAddressRequeride
    {
        public int idShippingAddress { get; set; }
        public string Name { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public int idCity { get; set; }
        public int idDepartment { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int idTypeClient { get; set; }
        public string nit { get; set; }
        public int idShippingUser { get; set; }

    }
    public class ShippingAddressResponse
    {
        public string api_dice { get; set; }
        public int idShippingAddress { get; set; }

    }
}

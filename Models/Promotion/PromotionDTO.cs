using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegronWebApi.Models.Promotion
{
    public class PromotionRequeride
    {
        public int idPromotion { get; set; }
        public int idPromotionType { get; set; }
        public int idProduct { get; set; }
        public int idCategory { get; set; }
        public string NamePromotion { get; set; }
        public string descriptionPromotion { get; set; }
        public string promotionCode { get; set; }
        public string condition { get; set; }
        public int couponAmount { get; set; }
        public int discountValue { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string idCitysPromotion { get; set; }
        public int idStore { get; set; }

    }
    public class PromotionTypeResponse
    {
        public int idPromotionType { get; set; }
        public string NamePromotionType { get; set; }
    }
    public class Promotion
    {
        public List<PromotionResponseUnit> ListPromotion { get; set; }

    }
    public class PromotionResponseUnit
    {

        public int idPromotion { get; set; }
        public string idPromotionType { get; set; }
        public string NamePromotionType { get; set; }
        public string idProduct { get; set; }
        public string NameProduct { get; set; }
        public string idCategory { get; set; }
        public string NameCategory { get; set; }
        public string NamePromotion { get; set; }
        public string descriptionPromotion { get; set; }
        public string promotionCode { get; set; }
        public string condition { get; set; }
        public string couponAmount { get; set; }
        public string discountValue { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string idStatusPromotion { get; set; }
        public string nameStatus { get; set; }
        public string dateAdd { get; set; }
        public string idStore { get; set; }
        public string country { get; set; }
        public List<DescriptionCities> ListCities { get; set; }

    }

    public class DescriptionCities
    {
        public string idCity { get; set; }
        public string City { get; set; }

    }
    public class PromotionResponse
    {
        public int idPromotion { get; set; }
        public string idPromotionType { get; set; }
        public string NamePromotionType { get; set; }
        public string idProduct { get; set; }
        public string NameProduct { get; set; }
        public string idCategory { get; set; }
        public string NameCategory { get; set; }
        public string NamePromotion { get; set; }
        public string descriptionPromotion { get; set; }
        public string promotionCode { get; set; }
        public string condition { get; set; }
        public string couponAmount { get; set; }
        public string discountValue { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string idStatusPromotion { get; set; }
        public string nameStatus { get; set; }
        public string dateAdd { get; set; }
        public string idStore { get; set; }
        public string country { get; set; }
        public string idCity { get; set; }
        public string City { get; set; }
    }
    public class StatusPromotionRequeride
    {
        public int Status { get; set; }
        public int idPromotion { get; set; }

    }
    public class StatusPromotionResponse
    {
        public int idStatusPromotion { get; set; }
        public string nameStatus { get; set; }

    }
}

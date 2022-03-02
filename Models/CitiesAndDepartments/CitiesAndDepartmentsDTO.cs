using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.CitiesAndDepartments
{
    public class CitiesResponse
    {
        public int idCity { get; set; }
        public string City { get; set; }
        public int idStore { get; set; }
    }

    public class ComunaResponse
    {
        public int COMUNA_ID { get; set; }
        public string COMUNA_NOMBRE { get; set; }
    }
    public class RegionResponse
    {
        public int REGION_ID { get; set; }
        public string REGION_NOMBRE { get; set; }
    }

    public class apiCitiesResponse
    {
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string city { get; set; }
        //public string postal { get; set; }
        //public string latitude { get; set; }
        //public string longitude { get; set; }
        //public string IPv4 { get; set; }
        public string state { get; set; }
    }
    public class DepartmentsResponse
    {
        public int idDepartment { get; set; }
        public string nameDepartment { get; set; }
    }
  
    public class GetDepartmentCitiesResponse
    {
        public int idCity { get; set; }
        public string City { get; set; }
        public int idDepartment { get; set; }
        public string nameDepartment { get; set; }
    }

    public class GetComunaByidregionResponse
    {
        public int COMUNA_ID { get; set; }
        public string COMUNA_NOMBRE { get; set; }
        public int REGION_ID { get; set; }
        public string REGION_NOMBRE { get; set; }

    }

    public class GetCountryResponse
    {
        public int idCountry { get; set; }
        public string country { get; set; }
        public decimal shippingPrice { get; set; }
        public string idTypeOfcurrency { get; set; }
        public string currency { get; set; }
        public string dateAdd { get; set; }
        public decimal squarePrice { get; set; }

    }
    public class GetCitiesByStoreResponse
    {
        public int idCity { get; set; }
        public string City { get; set; }
        public int idDepartment { get; set; }
        public int idStore { get; set; }


    }
    public class zohoRequeride
    {
        public string Email { get; set; }
    }
}

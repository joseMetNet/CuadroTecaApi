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

    public class GetCitiesByStoreResponse
    {
        public int idCity { get; set; }
        public string City { get; set; }
        public int idDepartment { get; set; }
        public int idStore { get; set; }


    }
}

using fotoTeca.Models.CitiesAndDepartments;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CitiesAndDepartmentsController : ControllerBase
    {
        private readonly CitiesAndDepartmentsDAL _repository1;
        public CitiesAndDepartmentsController(CitiesAndDepartmentsDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }
        [HttpGet("/getCities")]
        public async Task<List<CitiesResponse>> Get1()
        {
            return await _repository1.getCities();

        }
        [HttpGet("/getDepartments")]
        public async Task<List<DepartmentsResponse>> Get2()
        {
            return await _repository1.getDepartments();

        }
        [HttpGet("/GetDepartmentByCities/{idCity}")]
        public async Task<List<GetDepartmentCitiesResponse>> Get3(int idCity)
        {
            return await _repository1.GetDepartmentByCities(idCity);

        }
        [HttpGet("/GetCitiesByDepartment/{idDepartment}")]
        public async Task<List<GetDepartmentCitiesResponse>> Get4(int idDepartment)
        {
            return await _repository1.GetCitiesByDepartment(idDepartment);

        }
        [HttpGet("/GetCitiesByStore/{idStore}")]
        public async Task<List<GetCitiesByStoreResponse>> Get5(int idStore)
        {
            return await _repository1.GetCitiesByStore(idStore);

        }
    }
}

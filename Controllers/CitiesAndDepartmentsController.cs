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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CitiesAndDepartmentsController : ControllerBase
    {
        private readonly CitiesAndDepartmentsDAL _repository1;
        public CitiesAndDepartmentsController(CitiesAndDepartmentsDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }
        [HttpGet("/getUbication/{ip}")]
        public  async Task<apiCitiesResponse> Get01(string ip)
        {
            return await _repository1.ubication(ip);

        }
        [HttpGet("/getCities")]
        public async Task<List<CitiesResponse>> Get1()
        {
            return await _repository1.getCities();

        }
        //[HttpGet("/GetComuna")]
        //public async Task<List<ComunaResponse>> Get11()
        //{
        //    return await _repository1.GetComuna();

        //}

        //[HttpGet("/GetRegion")]
        //public async Task<List<RegionResponse>> Get111()
        //{
        //    return await _repository1.GetRegion();

        //}
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
        //[HttpGet("/GetComunaByidregion/{idRegion}")]
        //public async Task<List<GetComunaByidregionResponse>> Get33(int idRegion)
        //{
        //    return await _repository1.GetComunaByidregion(idRegion);

        //}
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
        [HttpGet("/GetCountry")]
        public async Task<List<GetCountryResponse>> Get6()
        {
            return await _repository1.GetCountry();

        }
        [HttpGet("/GetCountry/{idCountry}")]
        public async Task<List<GetCountryResponse>> Get7(int idCountry)
        {
            return await _repository1.GetCountry(idCountry);

        }

        [HttpPost("/zoho", Name = "zoho")]
        public async Task put3([FromForm] zohoRequeride us)

        {
            await _repository1.zoho(us);
        }
    }
}

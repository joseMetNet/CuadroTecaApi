
using fotoTeca.Email;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace fotoTeca.Models.CitiesAndDepartments
{
    public class CitiesAndDepartmentsDAL
    {
        private readonly string _connectionStrings;

        static HttpClient client = new HttpClient();

        public CitiesAndDepartmentsDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }
        public class apiCitiesResponse2
        {
            public string country_code { get; set; }
            public string country_name { get; set; }
            public string city { get; set; }
            public string postal { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string IPv4 { get; set; }
            public string state { get; set; }
        }
        //static async Task<apiCitiesResponse2> GetProductAsync(string path)
        //{
        //    apiCitiesResponse2 product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<apiCitiesResponse2>();
        //    }
        //    return product;
        //}
        //public  async Task<apiCitiesResponse> ubication()
        //{
        //    var url = $"https://geolocation-db.com/json";
        //    var request = (HttpWebRequest)WebRequest.Create(url);
        //    request.Method = "GET";
        //    request.ContentType = "application/json";
        //    request.Accept = "application/json";


        //    var response1 = new apiCitiesResponse();




        //    try
        //    {
        //        using (WebResponse response = request.GetResponse())
        //        {
        //            using (Stream strReader = response.GetResponseStream())
        //            {
        //                if (strReader == null) return response1;
        //                using (StreamReader objReader = new StreamReader(strReader))
        //                {
        //                    string responseBody = objReader.ReadToEnd();
        //                    // Do something with responseBody
        //                    Console.WriteLine(responseBody);
        //                }
        //            }
        //        }
        //    }
        //    catch (WebException ex)
        //    {
        //        // Handle error
        //    }

        //    return response1;


        //}


        //static async Task<apiCitiesResponse2> GetProductAsync(string path)
        //{
        //    apiCitiesResponse2 product = null;
        //    HttpResponseMessage response = await client.GetAsync(path);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        product = await response.Content.ReadAsAsync<apiCitiesResponse2>();
        //    }
        //    return product;
        //}



        public async Task<apiCitiesResponse> ubication(string ip)
        {
            using (var webClient = new System.Net.WebClient())
            {

                var data = webClient.DownloadString("https://geolocation-db.com/jsonp/" + ip);

                string[] subs = data.Split('(');

                var p = subs.GetValue(1);

                string[] p2 = p.ToString().Split(')');

                var p3 = p2.GetValue(0);

                JavaScriptSerializer jss = new JavaScriptSerializer();
                var d = jss.Deserialize<dynamic>(p3.ToString());

                string country_code = d["country_code"];
                string country_name = d["country_name"];
                string city = d["city"];
                string state = d["state"];

                apiCitiesResponse resp = new apiCitiesResponse();


                resp.country_code = country_code;

                resp.country_name = country_name;

                resp.city = city;

                resp.state = state;

                return resp;

            }

        }



        public async Task zoho(zohoRequeride req)
        {
            using (var webClient = new System.Net.WebClient())
            {

                //var data = webClient.DownloadString("https://forms.zohopublic.com/contacto169/form/EmailSubscription/formperma/3uX-zXG8phynVJZwgNrV2Ojfl54okfwQOcjgfnCKEag/htmlRecords/submit" + req );
                var client = new RestClient("https://forms.zohopublic.com/contacto169/form/EmailSubscription/formperma/3uX-zXG8phynVJZwgNrV2Ojfl54okfwQOcjgfnCKEag/htmlRecords/submit");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("Email", req.Email);
                IRestResponse response = client.Execute(request);

            }
        }
        public async Task<List<CitiesResponse>> getCities()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getCities", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var response = new List<CitiesResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue1(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public CitiesResponse MapToValue1(SqlDataReader reader)
        {
            return new CitiesResponse()
            {
                idCity = (int)reader["idCity"],
                City = reader["City"].ToString(),
                idStore = (int)reader["idStore"],

            };
        }

        public async Task<List<ComunaResponse>> GetComuna()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetComuna", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var response = new List<ComunaResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue22(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public ComunaResponse MapToValue22(SqlDataReader reader)
        {
            return new ComunaResponse()
            {
                COMUNA_ID = (int)reader["COMUNA_ID"],
                COMUNA_NOMBRE = reader["COMUNA_NOMBRE"].ToString(),
            };
        }

        public async Task<List<RegionResponse>> GetRegion()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetRegion", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var response = new List<RegionResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue11(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public RegionResponse MapToValue11(SqlDataReader reader)
        {
            return new RegionResponse()
            {
                REGION_ID = (int)reader["REGION_ID"],
                REGION_NOMBRE = reader["REGION_NOMBRE"].ToString(),
            };
        }

        public async Task<List<DepartmentsResponse>> getDepartments(int pidTypeUser = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getDepartments", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    var response = new List<DepartmentsResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue2(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public DepartmentsResponse MapToValue2(SqlDataReader reader)
        {
            return new DepartmentsResponse()
            {
                idDepartment = (int)reader["idDepartment"],
                nameDepartment = reader["nameDepartment"].ToString(),

            };
        }
        public async Task<List<GetDepartmentCitiesResponse>> GetDepartmentByCities(int idCity)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetDepartmentByCities", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PidCity", idCity));

                    var response = new List<GetDepartmentCitiesResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue3(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GetDepartmentCitiesResponse MapToValue3(SqlDataReader reader)
        {
            return new GetDepartmentCitiesResponse()
            {
                idCity = (int)reader["idCity"],
                City = reader["City"].ToString(),
                idDepartment = (int)reader["idDepartment"],
                nameDepartment = reader["nameDepartment"].ToString(),
            };
        }

        public async Task<List<GetComunaByidregionResponse>> GetComunaByidregion(int idRegion)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetComunaByidregion", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidRegion", idRegion));

                    var response = new List<GetComunaByidregionResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue33(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GetComunaByidregionResponse MapToValue33(SqlDataReader reader)
        {
            return new GetComunaByidregionResponse()
            {
                COMUNA_ID = (int)reader["COMUNA_ID"],
                COMUNA_NOMBRE = reader["COMUNA_NOMBRE"].ToString(),
                REGION_ID = (int)reader["REGION_ID"],
                REGION_NOMBRE = reader["REGION_NOMBRE"].ToString(),
            };
        }
        public async Task<List<GetDepartmentCitiesResponse>> GetCitiesByDepartment(int idDepartment)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetCitiesByDepartment", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidDepartment", idDepartment));

                    var response = new List<GetDepartmentCitiesResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue4(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GetDepartmentCitiesResponse MapToValue4(SqlDataReader reader)
        {
            return new GetDepartmentCitiesResponse()
            {
                idCity = (int)reader["idCity"],
                City = reader["City"].ToString(),
                idDepartment = (int)reader["idDepartment"],
                nameDepartment = reader["nameDepartment"].ToString(),
            };
        }

        public async Task<List<GetCitiesByStoreResponse>> GetCitiesByStore(int idStore)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetCitiesByStore", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidStore", idStore));

                    var response = new List<GetCitiesByStoreResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue5(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GetCitiesByStoreResponse MapToValue5(SqlDataReader reader)
        {
            return new GetCitiesByStoreResponse()
            {
                idCity = (int)reader["idCity"],
                City = reader["City"].ToString(),
                idDepartment = (int)reader["idDepartment"],
                idStore = (int)reader["idStore"],
            };
        }

        public async Task<List<GetCountryResponse>> GetCountry(int idCountry = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetCountry", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idCountry > 0) { cmd.Parameters.Add(new SqlParameter("@pidCountry", idCountry)); }

                    var response = new List<GetCountryResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue6(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GetCountryResponse MapToValue6(SqlDataReader reader)
        {
            return new GetCountryResponse()
            {
                idCountry = (int)reader["idCountry"],
                country = reader["country"].ToString(),
                shippingPrice = (decimal)reader["shippingPrice"],
                idTypeOfcurrency = reader["idTypeOfcurrency"].ToString(),
                currency = reader["currency"].ToString(),
                dateAdd = reader["dateAdd"].ToString(),
                squarePrice = (decimal)reader["squarePrice"],
            };
        }
    }
}

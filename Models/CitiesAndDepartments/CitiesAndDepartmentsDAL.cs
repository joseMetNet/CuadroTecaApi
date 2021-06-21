using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.CitiesAndDepartments
{
    public class CitiesAndDepartmentsDAL
    {
        private readonly string _connectionStrings;

        public CitiesAndDepartmentsDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

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
    }
}

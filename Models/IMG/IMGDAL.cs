using fotoTeca.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.IMG
{
    public class IMGDAL
    {
        private readonly string _connectionStrings;
        CredencialesDeAcceso acceso = new CredencialesDeAcceso();

        public IMGDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");
        }

          public async Task<List<IMGResponse>> GetIMGType1(int idIMG = 0)
          {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetIMGType1", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idIMG != 0) cmd.Parameters.Add(new SqlParameter("@pidIMG", idIMG));

                    var response = new List<IMGResponse>();
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

        public IMGResponse MapToValue1(SqlDataReader reader)
        {
            return new IMGResponse()
            {
                Id = (int)reader["Id"],
                IMGType1 = reader["IMG1"].ToString(),
            };
        }
        public async Task<List<IMGResponse2>> GetIMGType2(int idIMG = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetIMGType2", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idIMG != 0) cmd.Parameters.Add(new SqlParameter("@pidIMG", idIMG));

                    var response = new List<IMGResponse2>();
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

        public IMGResponse2 MapToValue2(SqlDataReader reader)
        {
            return new IMGResponse2()
            {
                Id = (int)reader["Id"],
                IMGType2 = reader["IMG2"].ToString(),
            };
        }

        public async Task<List<IMGResponse3>> GetIMGTotality(int idIMG = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetIMGTotality", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idIMG != 0) cmd.Parameters.Add(new SqlParameter("@pidIMG", idIMG));

                    var response = new List<IMGResponse3>();
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

        public IMGResponse3 MapToValue3(SqlDataReader reader)
        {
            return new IMGResponse3()
            {
                Id = (int)reader["Id"],
                IMGType1 = reader["IMG1"].ToString(),
                IMGType2 = reader["IMG2"].ToString(),
            };
        }
    }
}

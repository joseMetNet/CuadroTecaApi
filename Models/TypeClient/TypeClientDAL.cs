using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.TypeClient
{
    public class TypeClientDAL
    {
        private readonly string _connectionStrings;


        public TypeClientDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");
        }
        public async Task<List<TypeClientResponse>> GetTypeClient()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetTypeClient", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    var response = new List<TypeClientResponse>();
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

        public TypeClientResponse MapToValue2(SqlDataReader reader)
        {
            return new TypeClientResponse()
            {
                idTypeClient = (int)reader["idTypeClient"],
                NameTypeClient = reader["NameTypeClient"].ToString()
            };
        }
    }
}

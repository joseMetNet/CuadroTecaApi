using fotoTeca.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Platform
{
    public class StoreDAL
    {
        private readonly string _connectionStrings;
        CredencialesDeAcceso acceso = new CredencialesDeAcceso();

        public StoreDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }
        public async Task<List<StoreResponse>> GetStore(int idStore = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetStore", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idStore != 0) cmd.Parameters.Add(new SqlParameter("@pidStore", idStore));

                    var response = new List<StoreResponse>();
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

        public StoreResponse MapToValue1(SqlDataReader reader)
        {
            return new StoreResponse()
            {
                idStore = (int)reader["idStore"],
                NameStore = reader["NameStore"].ToString(),
                country = reader["country"].ToString(),
                priceProduct = (decimal)reader["priceProduct"],
                PriceShipping = (decimal)reader["PriceShipping"],

            };
        }
    }
}

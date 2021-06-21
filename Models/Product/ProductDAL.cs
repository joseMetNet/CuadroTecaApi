using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Product
{
    public class ProductDAL
    {
        private readonly string _connectionStrings;
     

        public ProductDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }
        public async Task<List<ProductResponse>> GetProduct(int idProduct = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetProduct", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idProduct != 0) cmd.Parameters.Add(new SqlParameter("@pidProduct", idProduct));


                    var response = new List<ProductResponse>();
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

        public ProductResponse MapToValue3(SqlDataReader reader)
        {
            return new ProductResponse()
            {
                idProduct = (int)reader["idProduct"],
                NameProduct = reader["NameProduct"].ToString(),
                dateAdd = reader["dateAdd"].ToString(),
            };

        }

        public async Task<List<ProductResponseCategory>> GetCategory()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetCategory", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<ProductResponseCategory>();
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

        public ProductResponseCategory MapToValue4(SqlDataReader reader)
        {
            return new ProductResponseCategory()
            {
                idCategory = (int)reader["idCategory"],
                NameCategory = reader["NameCategory"].ToString(),
            };
        }

    }
}

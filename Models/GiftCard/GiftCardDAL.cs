using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.GiftCard
{
    public class GiftCardDAL
    {
        private readonly string _connectionStrings;

        public GiftCardDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }
        public async Task<List<GiftCarResponse>> storeOrUpdateGiftCard(GiftCarResqueride Gift)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateGiftCard", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //Para crear este parametro idUser lo tendrian que enviar como 0 no como nulo ni vacio
                    if (Gift.idGiftCard != 0) cmd.Parameters.Add(new SqlParameter("@pidGiftCard", Gift.idGiftCard));
                    cmd.Parameters.Add(new SqlParameter("@pNameGiftCard", Gift.NameGiftCard));
                    cmd.Parameters.Add(new SqlParameter("@pReference", Gift.reference));
                    cmd.Parameters.Add(new SqlParameter("@pValue", Gift.value));
       

                    var response = new List<GiftCarResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GiftCarResponse MapToValue(SqlDataReader reader)
        {
            return new GiftCarResponse()
            {
        
                api_Dice = reader["api_Dice"].ToString(),
                idGiftCard = (int)reader["idGiftCard"],
                NameGiftCard = reader["NameGiftCard"].ToString(),
                idStatusGiftCard = (int)reader["idStatusGiftCard"],
                nameStatus = reader["nameStatus"].ToString(),

            };
        }

        public async Task<List<GiftCarResponse2>> GetGiftCard(int idGiftCard = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetGiftCard", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idGiftCard != 0) cmd.Parameters.Add(new SqlParameter("@pidGiftCard", idGiftCard));



                    var response = new List<GiftCarResponse2>();
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

        public GiftCarResponse2 MapToValue3(SqlDataReader reader)
        {
            return new GiftCarResponse2()
            {
                idGiftCard = (int)reader["idGiftCard"],
                NameGiftCard = reader["NameGiftCard"].ToString(),
                reference = reader["reference"].ToString(),
                value = (decimal)reader["value"],
                idStatusGiftCard = (int)reader["idStatusGiftCard"],
                Status = reader["Status"].ToString(),
                dateAdd = reader["dateAdd"].ToString(),
            };

        }

        public async Task DeleteGiftCard(int idGiftCard)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteGiftCard", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidGiftCard", idGiftCard));


                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }

        }
        public async Task UpdateGiftCard(GiftCarStatusResqueride apu)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateGiftCard", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pStatus", apu.idStatus));
                    cmd.Parameters.Add(new SqlParameter("@pidGiftCard", apu.idGiftCard));




                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }

        }
    }
}

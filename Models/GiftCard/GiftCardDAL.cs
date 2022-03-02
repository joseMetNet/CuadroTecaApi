using fotoTeca.Email;
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
                    cmd.Parameters.Add(new SqlParameter("@pIVA", Gift.IVA));




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
                IVA = (int)reader["IVA"],

            };

        }

        public async Task<List<GiftCarByCodeResponse>> getGiftCardBycode(string CodeGitfCard )
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getGiftCardBycode", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pCodeGitfCard", CodeGitfCard));



                    var response = new List<GiftCarByCodeResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValues(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public GiftCarByCodeResponse MapToValues(SqlDataReader reader)
        {
            return new GiftCarByCodeResponse()
            {
                idGiftCard = (int)reader["idGiftCard"],
                value = (decimal)reader["value"],
                DateExpiration = reader["DateExpiration"].ToString(),
                idStatusGiftCard = reader["idStatusGiftCard"].ToString(),
                nameStatus = reader["nameStatus"].ToString(),
                balance = (int)reader["balance"],
            };

        }
        public async Task<List<GiftCarResponse2>> GetGiftCardActive()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetGiftCardActive", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;



                    var response = new List<GiftCarResponse2>();
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

        public GiftCarResponse2 MapToValue4(SqlDataReader reader)
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

        public async Task<List<SalesGiftCardResponse>> GetSalesGiftCard()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetSalesGiftCard", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var response = new List<SalesGiftCardResponse>();
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

        public SalesGiftCardResponse MapToValue5(SqlDataReader reader)
        {
            return new SalesGiftCardResponse()
            {
                idOrder = (int)reader["idOrder"],
                idGiftCard = reader["idGiftCard"].ToString(),
                value = reader["value"].ToString(),
                dateAdd = reader["dateAdd"].ToString(),
                invoice = reader["invoice"].ToString(),
                idStatusPayOrder = reader["idStatusPayOrder"].ToString(),
                FullNameBuyer = reader["FullNameBuyer"].ToString(),
                FullNameAddressee = reader["FullNameAddressee"].ToString(),
                EamilBuyer = reader["EamilBuyer"].ToString(),
                EmailAddressee = reader["EmailAddressee"].ToString(),
                DateExpiration = reader["DateExpiration"].ToString(),
                currentDate = reader["currentDate"].ToString(),
                StatusPoll = reader["StatusPoll"].ToString(),
                surveyResult = reader["surveyResult"].ToString(),
                StatusEmailSale = reader["StatusEmailSale"].ToString(),
                NameStatus = reader["NameStatus"].ToString(),
                UsedGiftCard = reader["UsedGiftCard"].ToString(),
                NamePromotion = reader["NamePromotion"].ToString(),
                idPromotion = reader["idPromotion"].ToString(),
                idStatusOrder = reader["idStatusOrder"].ToString(),
                NameStatusOrder = reader["NameStatusOrder"].ToString(),
                typeSale = reader["typeSale"].ToString(),


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

        public async Task GetPendingToUseGiftCard()
        {
            
                using (SqlConnection sql = new SqlConnection(_connectionStrings))
                {   

                    List<dataPendign> EmailAdmins = new List<dataPendign>();
                    using (SqlCommand cmd = new SqlCommand("SP_GetPendingToUseGiftCard", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        await sql.OpenAsync();


                    //await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                EmailAdmins.Add(new dataPendign
                                {
                                    email = reader["email"].ToString()
                                    ,
                                    nombreResibe = reader["nombreResibe"].ToString()
                                    ,
                                    nombreComprador = reader["nombreComprador"].ToString()
                                    ,
                                    codeGitfCard = reader["codeGitfCard"].ToString()

                                });
                            }
                        

                        }
                    }

                    string Cuerpomail = "";
                    EnviarEMail em = new EnviarEMail();

                    
       
                    foreach (dataPendign row2 in EmailAdmins)
                    {
                        Cuerpomail = em.emailPendingToUseGiftCard(row2.nombreResibe, row2.nombreComprador, row2.codeGitfCard);
                        em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado un cambio de estado en un producto en la plataforma", row2.email);
                    }
                }
            


            return;

        }
    }
}

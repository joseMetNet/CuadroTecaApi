using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NegronWebApi.Models.Promotion
{
    public class PromotionDAL
    {
        private readonly string _connectionStrings;

        public PromotionDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");
        }


        public async Task storeOrUpdatePromotion(PromotionRequeride req)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdatePromotion", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (req.idPromotion != 0) cmd.Parameters.Add(new SqlParameter("@pidPromotion", req.idPromotion));
                    cmd.Parameters.Add(new SqlParameter("@pidPromotionType", req.idPromotionType));
                    cmd.Parameters.Add(new SqlParameter("@pidProduct", req.idProduct));
                    cmd.Parameters.Add(new SqlParameter("@pidCategory", req.idCategory));
                    cmd.Parameters.Add(new SqlParameter("@pNamePromotion", req.NamePromotion));
                    cmd.Parameters.Add(new SqlParameter("@pDescriptionPromotion", req.descriptionPromotion));
                    cmd.Parameters.Add(new SqlParameter("@pPromotionCode", req.promotionCode));
                    cmd.Parameters.Add(new SqlParameter("@pCondition", req.condition));
                    cmd.Parameters.Add(new SqlParameter("@pCouponAmount", req.couponAmount));
                    cmd.Parameters.Add(new SqlParameter("@pDiscountValue", req.discountValue));
                    cmd.Parameters.Add(new SqlParameter("@pStartDate", req.startDate));
                    cmd.Parameters.Add(new SqlParameter("@pEndDate", req.endDate));
                    cmd.Parameters.Add(new SqlParameter("@pNameCitysPromotion", req.idCitysPromotion));
                    cmd.Parameters.Add(new SqlParameter("@pidStore", req.idStore));


                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }

        }

        public async Task<List<PromotionTypeResponse>> getPromotionType()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getPromotionType", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;



                    var response = new List<PromotionTypeResponse>();
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


        public PromotionTypeResponse MapToValue1(SqlDataReader reader)
        {
            return new PromotionTypeResponse()
            {
                idPromotionType = (int)reader["idPromotionType"],
                NamePromotionType = reader["NamePromotionType"].ToString(),

            };
        }
        public async Task<Promotion> getPromotion(int idPromotion = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getPromotion", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idPromotion != 0) cmd.Parameters.Add(new SqlParameter("@pidPromotion", idPromotion));


                    var response = new List<PromotionResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue2(reader));

                        }

                    }

                    response = response.OrderBy(e => e.idPromotion).ToList();
                    var result = new Promotion();
                    result.ListPromotion = new List<PromotionResponseUnit>();
                    int idPromotioN = 0;
                    int idPromotionAnt = 0;
                    for (int i = 0; i < response.Count; i++)
                    {
                        PromotionResponse row = response[i];
                        idPromotioN = row.idPromotion;
                        var objform = new PromotionResponseUnit();
                        objform.idPromotion = row.idPromotion;
                        objform.idPromotionType = row.idPromotionType;
                        objform.NamePromotionType = row.NamePromotionType;
                        objform.idProduct = row.idProduct;
                        objform.NameProduct = row.NameProduct;
                        objform.idCategory = row.idCategory;
                        objform.NameCategory = row.NameCategory;
                        objform.NamePromotion = row.NamePromotion;
                        objform.descriptionPromotion = row.descriptionPromotion;
                        objform.promotionCode = row.promotionCode;
                        objform.condition = row.condition;
                        objform.couponAmount = row.couponAmount;
                        objform.discountValue = row.discountValue;
                        objform.startDate = row.startDate;
                        objform.endDate = row.endDate;
                        objform.idStatusPromotion = row.idStatusPromotion;
                        objform.nameStatus = row.nameStatus;
                        objform.dateAdd = row.dateAdd;
                        objform.idStore = row.idStore;
                        objform.country = row.country;



                        objform.ListCities = new List<DescriptionCities>();
                        if (idPromotioN != idPromotionAnt)
                        {
                            foreach (PromotionResponse row2 in response)
                            {
                                if (row.idPromotion == row2.idPromotion)
                                {
                                    objform.ListCities.Add(
                                        new DescriptionCities
                                        {
                                            idCity = row2.idCity,
                                            City = row2.City,

                                        }
                                    );
                                }
                            }
                            result.ListPromotion.Add(objform);
                        }
                        idPromotionAnt = row.idPromotion;
                    }
                    return result;
                }

            }

        }
        //SELECT
        //           ci.idCity

        //           cy, city

        //          FROM TB_promotion as qq
        //          LEFT JOIN TB_CitiesPromotion AS ci On ci.idPromotion = qq.idPromotion

        //          LEFT JOIN TB_City AS cy On cy.idCity = ci.idCity
        public PromotionResponse MapToValue2(SqlDataReader reader)
        {
            return new PromotionResponse()
            {
                idPromotion = (int)reader["idPromotion"],
                idPromotionType = reader["idPromotionType"].ToString(),
                NamePromotionType = reader["NamePromotionType"].ToString(),
                idProduct = reader["idProduct"].ToString(),
                NameProduct = reader["NameProduct"].ToString(),
                idCategory = reader["idCategory"].ToString(),
                NameCategory = reader["NameCategory"].ToString(),
                NamePromotion = reader["NamePromotion"].ToString(),
                descriptionPromotion = reader["descriptionPromotion"].ToString(),
                promotionCode = reader["promotionCode"].ToString(),
                condition = reader["condition"].ToString(),
                couponAmount = reader["couponAmount"].ToString(),
                idStore = reader["idStore"].ToString(),
                country = reader["country"].ToString(),
                discountValue = reader["discountValue"].ToString(),
                startDate = reader["startDate"].ToString(),
                endDate = reader["endDate"].ToString(),
                idStatusPromotion = reader["idStatusPromotion"].ToString(),
                nameStatus = reader["nameStatus"].ToString(),
                dateAdd = reader["dateAdd"].ToString(),
                idCity = reader["idCity"].ToString(),
                City = reader["City"].ToString(),

            };
        }

        public async Task<List<StatusPromotionResponse>> getStatusPromotion()
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getStatusPromotion", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;



                    var response = new List<StatusPromotionResponse>();
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
        public StatusPromotionResponse MapToValue3(SqlDataReader reader)
        {
            return new StatusPromotionResponse()
            {
                idStatusPromotion = (int)reader["idStatusPromotion"],
                nameStatus = reader["nameStatus"].ToString(),

            };
        }


        public async Task UpdateStatusPromotion(StatusPromotionRequeride use)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateStatusPromotion", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Status", use.Status));
                    cmd.Parameters.Add(new SqlParameter("@pidPromotion", use.idPromotion));



                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }

        }

    }
}

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Dashboard
{
    public class DashboardDAL
    {
        private readonly string _connectionStrings;

        public DashboardDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }

        public async Task<List<storeOrUpdateVisitsWebResponse>> storeOrUpdateVisitsWeb(storeOrUpdateVisitsWebRequeride cli)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateVisitsWeb", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (cli.idVisitsWeb != 0) cmd.Parameters.Add(new SqlParameter("@pidVisitsWeb", cli.idVisitsWeb));
                    cmd.Parameters.Add(new SqlParameter("@pCity", cli.City));
                    cmd.Parameters.Add(new SqlParameter("@pPossiblePurchase", cli.PossiblePurchase));
                    var response = new List<storeOrUpdateVisitsWebResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue14(reader));
                        }
                    }

                    return response;
                }

            }

        }

        public storeOrUpdateVisitsWebResponse MapToValue14(SqlDataReader reader)
        {
            return new storeOrUpdateVisitsWebResponse()
            {
                idVisitsWeb = (int)reader["idVisitsWeb"],
                possiblePurchase = reader["possiblePurchase"].ToString(),
                City = reader["City"].ToString(),
            };
        }

        public async Task<List<getTotalVisitsWebResponse>> getTotalVisitsWeb(string date1 = "1000-00-00", string date2 = "1000-00-00")
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_getTotalVisitsWeb", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (date1 != "1000-00-00") { cmd.Parameters.Add(new SqlParameter("@pDate1", date1)); }
                    if (date2 != "1000-00-00") { cmd.Parameters.Add(new SqlParameter("@pDate2", date2)); }

                    var response = new List<getTotalVisitsWebResponse>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue15(reader));
                        }
                    }
                    return response;
                }

            }

        }



        public getTotalVisitsWebResponse MapToValue15(SqlDataReader reader)
        {
            return new getTotalVisitsWebResponse()
            {
                TotalVisitsWeb = reader["TotalVisitsWeb"].ToString(),
            };
        }

        public async Task<List<GetSalesDashboardResponse>> GetSalesDashboard( string date1 = "1000-00-00", string date2 = "1000-00-00")
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetSalesDashboard", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (date1 != "1000-00-00") { cmd.Parameters.Add(new SqlParameter("@pDate1", date1)); }
                    if (date2 != "1000-00-00") { cmd.Parameters.Add(new SqlParameter("@pDate2", date2)); }

                    var response = new List<GetSalesDashboardResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue18(reader));
                        }

                    }

                    return response;
                }

            }

        }
        public GetSalesDashboardResponse MapToValue18(SqlDataReader reader)
        {
            return new GetSalesDashboardResponse()
            {
                salesToday = reader["salesToday"].ToString(),
                SalesInproduction = reader["SalesInproduction"].ToString(),
                SalesDispatched = reader["SalesDispatched"].ToString(),
                SalesDelivered = reader["SalesDelivered"].ToString(),
                TotalsOrders = reader["TotalsOrders"].ToString(),
                SalesTotals = reader["SalesTotals"].ToString(),
                Time_Dispatched_production = reader["Time_Dispatched_production"].ToString(),
                Time_Dispatched_average = reader["Time_Dispatched_production"].ToString(),
            };
        }

        public async Task<List<SalesPerDayResponse>> GetSalesPerDay(string date1 = "1000-00-00", string date2 = "1000-00-00")
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetSalesPerDay", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (date1 != "1000-00-00") { cmd.Parameters.Add(new SqlParameter("@pDate1", date1)); }
                    if (date2 != "1000-00-00") { cmd.Parameters.Add(new SqlParameter("@pDate2", date2)); }


                    var response = new List<SalesPerDayResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue19(reader));
                        }

                    }

                    return response;
                }

            }

        }
        public SalesPerDayResponse MapToValue19(SqlDataReader reader)
        {
            return new SalesPerDayResponse()
            {
                idOrder = (int)reader["idOrder"],
                dateAdd = reader["dateAdd"].ToString(),
                quantity = reader["quantity"].ToString(),
            };
        }
        public async Task<List<OrdersManagementbResponse>> GetOrdersManagement( int idOrder = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetOrdersManagement", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idOrder != 0) cmd.Parameters.Add(new SqlParameter("@pidOrder", idOrder));


                    var response = new List<OrdersManagementbResponse>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue16(reader));
                        }
                    }
                    return response;
                }

            }

        }

        public OrdersManagementbResponse MapToValue16(SqlDataReader reader)
        {
            return new OrdersManagementbResponse()
            {
                idOrder = (int)reader["idOrder"],
                idTable = reader["idTable"].ToString(),
                priceTotality = reader["priceTotality"].ToString(),
                dateAdd = reader["dateAdd"].ToString(),
                IdMercadoPago = reader["IdMercadoPago"].ToString(),
                invoice = reader["invoice"].ToString(),
                Name = reader["Name"].ToString(),
                idPromotion = reader["idPromotion"].ToString(),
                NamePromotion = reader["NamePromotion"].ToString(),
                Fullname = reader["Fullname"].ToString(),
                email = reader["email"].ToString(),
                FullNameBuyer = reader["FullNameBuyer"].ToString(),
                TypeSale = reader["TypeSale"].ToString(),
                numberFrames = reader["numberFrames"].ToString(),
                NumberMessage = reader["NumberMessage"].ToString(),
                idStatusOrder = reader["idStatusOrder"].ToString(),
                NameStatus = reader["NameStatus"].ToString(),
                idUser = reader["idUser"].ToString(),
                NameOperator = reader["NameOperator"].ToString(),
                conveyor = reader["conveyor"].ToString(),
                guide = reader["guide"].ToString(),
                MaximumDate = reader["MaximumDate"].ToString(),
                StatusEmailSale = reader["StatusEmailSale"].ToString(),
                StatusEmailShipping = reader["StatusEmailShipping"].ToString(),
                StatusEmailDelivery = reader["StatusEmailDelivery"].ToString(),
                StatusPoll = reader["StatusPoll"].ToString(),
                surveyResult = reader["surveyResult"].ToString(),
                country = reader["country"].ToString(),
                ActualDeliveryDate = reader["ActualDeliveryDate"].ToString(),
            };
        }

        public async Task UpdateOrder(UpdateOrderRequeride use)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateOrder", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidOrder", use.idOrder));
                    cmd.Parameters.Add(new SqlParameter("@pStatusEmailSale", use.StatusEmailSale));
                    cmd.Parameters.Add(new SqlParameter("@pStatusEmailShipping", use.StatusEmailShipping));
                    cmd.Parameters.Add(new SqlParameter("@pStatusEmailDelivery", use.StatusEmailDelivery));
                    cmd.Parameters.Add(new SqlParameter("@pStatusPoll", use.StatusPoll));
                    cmd.Parameters.Add(new SqlParameter("@pSurveyResult", use.surveyResult));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
            }
        }

        public async Task UpdateOrderProduction(UpdateOrderProductionRequeride use)
        {
            for (int i = 0; i < use.idOrder.Split(',').Length; i++)
            {
                using (SqlConnection sql = new SqlConnection(_connectionStrings))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateOrderProduction", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pidOrder", use.idOrder.Split(',')[i].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pinvoice", use.invoice));
                        cmd.Parameters.Add(new SqlParameter("@pidStatus", use.idStatus));
                        cmd.Parameters.Add(new SqlParameter("@pidUser", use.idUser));
                        cmd.Parameters.Add(new SqlParameter("@pConveyor", use.Conveyor));
                        cmd.Parameters.Add(new SqlParameter("@pGuide", use.Guide));
                        cmd.Parameters.Add(new SqlParameter("@pActualDeliveryDate", use.ActualDeliveryDate));

                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
            }
            return;

        }
    }
}

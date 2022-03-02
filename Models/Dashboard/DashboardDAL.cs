using fotoTeca.Email;
using fotoTeca.Models.Product;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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
                idGiftCardUsedForPurchase = reader["idGiftCardUsedForPurchase"].ToString(),
                ValueGiftCardUsedForPurchase = reader["ValueGiftCardUsedForPurchase"].ToString(),
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
                DateStatus6 = reader["DateStatus6"].ToString(),
                MaximumDateDelivery = reader["MaximumDateDelivery"].ToString(),
                StatusEmailSale = reader["StatusEmailSale"].ToString(),
                StatusEmailShipping = reader["StatusEmailShipping"].ToString(),
                StatusEmailDelivery = reader["StatusEmailDelivery"].ToString(),
                StatusPoll = reader["StatusPoll"].ToString(),
                surveyResult = reader["surveyResult"].ToString(),
                country = reader["country"].ToString(),
                ActualDeliveryDate = reader["ActualDeliveryDate"].ToString(),
                frame = reader["frame"].ToString(),
                priceShipment = reader["priceShipment"].ToString(),
                iva = reader["iva"].ToString(),
               

            };
        }


        //public async Task<Management> GetOrdersManagementByid(int idOrder)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionStrings))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SP_GetOrdersManagement", sql))
        //        {

        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.Parameters.Add(new SqlParameter("@pidOrder", idOrder));



        //            var response = new List<OrdersManagementbResponse2>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValue17(reader));
        //                }

        //            }
        //            response = response.OrderBy(e => e.idOrder).ToList();
        //            var result = new Management();
        //            result.ListsManagement = new List<ManagementUnit>();
        //            int idProductL = 0;
        //            int idProductAnt = 0;
        //            for (int i = 0; i < response.Count; i++)
        //            {
        //                OrdersManagementbResponse2 row = response[i];
        //                idProductL = row.idOrder;
        //                var objform = new ManagementUnit();
        //                objform.idOrder = row.idOrder;
        //                objform.idTable = row.idTable;
        //                objform.priceTotality = row.priceTotality;
        //                objform.dateAdd = row.dateAdd;
        //                objform.IdMercadoPago = row.IdMercadoPago;
        //                objform.invoice = row.invoice;
        //                objform.Name = row.Name;
        //                objform.idPromotion = row.idPromotion;
        //                objform.NamePromotion = row.NamePromotion;
        //                objform.Fullname = row.Fullname;
        //                objform.email = row.email;
        //                objform.FullNameBuyer = row.FullNameBuyer;
        //                objform.addressDelivery = row.addressDelivery;
        //                objform.idUser = row.idUser;
        //                objform.TypeSale = row.TypeSale;
        //                objform.numberFrames = row.numberFrames;
        //                objform.NumberMessage = row.NumberMessage;
        //                objform.idStatusOrder = row.idStatusOrder;
        //                objform.NameStatus = row.NameStatus;
        //                objform.idUser = row.idUser;
        //                objform.NameOperator = row.NameOperator;
        //                objform.conveyor = row.conveyor;
        //                objform.guide = row.guide;
        //                objform.MaximumDate = row.MaximumDate;
        //                objform.DateStatus6 = row.DateStatus6;
        //                objform.StatusEmailSale = row.StatusEmailSale;
        //                objform.StatusEmailShipping = row.StatusEmailShipping;
        //                objform.StatusEmailDelivery = row.StatusEmailDelivery;
        //                objform.StatusPoll = row.StatusPoll;
        //                objform.surveyResult = row.surveyResult;
        //                objform.country = row.country;
        //                objform.ActualDeliveryDate = row.ActualDeliveryDate;
        //                objform.frame = row.frame;
        //                objform.idCity = row.idCity;
        //                objform.City = row.City;
        //                objform.edge = row.edge;
        //                objform.address = row.address;
        //                objform.DescriptionManagemenM = new List<DescriptionManagemen1>();

        //                if (idProductL != idProductAnt)
        //                {
        //                    foreach (OrdersManagementbResponse2 row2 in response)
        //                    {
        //                        if (row.idOrder == row2.idOrder)
        //                        {
        //                            objform.DescriptionManagemenM.Add(
        //                                new DescriptionManagemen1
        //                                {
        //                                    editUrl = row2.editUrl,
        //                                }
        //                            );
        //                        }
        //                    }

        //                    result.ListsManagement.Add(objform);
        //                }
        //                idProductAnt = row.idOrder;
        //            }
        //            return result;
        //        }

        //    }

        //}

        public async Task<ManagementGeneral> GetOrdersManagementByid(int idOrder = 0)
        {

            var numeroorden = idOrder;
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                var response = new ManagementGeneral();
                var objRow = new List<OrdersManagementbResponse2>();

                using (SqlCommand cmd = new SqlCommand("SP_GetOrdersManagement", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idOrder != 0) cmd.Parameters.Add(new SqlParameter("@pidOrder", idOrder));

                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            objRow.Add(MapToValue17(reader));
                        }
                    }
                    objRow = objRow.OrderBy(e => e.idOrder).ToList();
                    int idPromotioN = 0;
                    int idPromotionAnt = 0;
                    for (int i = 0; i < objRow.Count; i++)
                    {
                        OrdersManagementbResponse2 row = objRow[i];
                        idOrder = Convert.ToInt32(row.idOrder);
                        response.idOrder = row.idOrder;
                        response.idTable = row.idTable;
                        response.priceTotality = row.priceTotality;
                        response.dateAdd = row.dateAdd;
                        response.IdMercadoPago = row.IdMercadoPago;
                        response.invoice = row.invoice;
                        response.Name = row.Name;
                        response.idPromotion = row.idPromotion;
                        response.NamePromotion = row.NamePromotion;
                        response.Fullname = row.Fullname;
                        response.email = row.email;
                        response.FullNameBuyer = row.FullNameBuyer;
                        response.idUser = row.idUser;
                        response.TypeSale = row.TypeSale;
                        response.numberFrames = row.numberFrames;
                        response.NumberMessage = row.NumberMessage;
                        response.idStatusOrder = row.idStatusOrder;
                        response.NameStatus = row.NameStatus;
                        response.idUser = row.idUser;
                        response.NameOperator = row.NameOperator;
                        response.conveyor = row.conveyor;
                        response.guide = row.guide;
                        response.MaximumDate = row.MaximumDate;
                        response.DateStatus6 = row.DateStatus6;
                        response.StatusEmailSale = row.StatusEmailSale;
                        response.StatusEmailShipping = row.StatusEmailShipping;
                        response.StatusEmailDelivery = row.StatusEmailDelivery;
                        response.StatusPoll = row.StatusPoll;
                        response.surveyResult = row.surveyResult;
                        response.country = row.country;
                        response.ActualDeliveryDate = row.ActualDeliveryDate;
                        response.frame = row.frame;
                        response.idCity = row.idCity;
                        response.City = row.City;
                        response.edge = row.edge;
                        response.address = row.address;
                        response.idShippingUser = row.idShippingUser;
                        response.DescriptionManagemenM = new List<DescriptionManagemen1>();
                        response.DescriptionShippingUser = new List<ListShippingUser>();
                        response.DescriptionAddressUser = new List<ListEmailAddressUser>();



                        if (idOrder != idPromotionAnt)
                        {
                            foreach (OrdersManagementbResponse2 row2 in objRow)
                            {
                                if (row.idOrder == row2.idOrder)
                                {
                                    response.DescriptionManagemenM.Add(
                                        new DescriptionManagemen1
                                        {
                                            editUrl = row2.editUrl,
                                        }
                                    );
                                }
                            }
                        }
                    }

                }

                using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailShippingUser", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidOrder", numeroorden));


                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.DescriptionShippingUser.Add(
                                new ListShippingUser
                                {
                                    idShippingUser = (int)reader["idShippingUser"]
                                    ,
                                    idOrder = reader["idOrder"].ToString()
                                    ,
                                    Fullname = reader["Fullname"].ToString()
                                    ,
                                    email = reader["email"].ToString()
                                    ,
                                    NameStatus = reader["NameStatus"].ToString()
                                    ,
                                    present = reader["present"].ToString()
                                    ,
                                    address = reader["address"].ToString()
                                    ,
                                    phone = reader["phone"].ToString()
                                    ,
                                    idCity = reader["idCity"].ToString()
                                    ,
                                    NameCity = reader["NameCity"].ToString()

                                }
                            );
                        }
                    }
                }

                ListShippingUser pass = new ListShippingUser();


                using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailAddressUser", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idOrder != 0) cmd.Parameters.Add(new SqlParameter("@pidOrder", numeroorden));
                    if (idOrder != 0) cmd.Parameters.Add(new SqlParameter("@pidShippingUser", response.idShippingUser));



                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.DescriptionAddressUser.Add(
                                new ListEmailAddressUser
                                {
                                    idShippingAddress = (int)reader["idShippingAddress"]
                                    ,
                                    FullName = reader["FullName"].ToString()
                                    ,
                                    email = reader["email"].ToString()
                                    ,
                                    address = reader["address"].ToString()
                                    ,
                                    idCity = reader["idCity"].ToString()
                                    ,
                                    Phone = reader["Phone"].ToString()
                                    ,
                                    City = reader["City"].ToString()
                                    

                                }
                            );
                        }
                    }
                }

                return response;
            }
        }


        public OrdersManagementbResponse2 MapToValue17(SqlDataReader reader)
        {
            return new OrdersManagementbResponse2()
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
                //addressDelivery = reader["addressDelivery"].ToString(),
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
                DateStatus6 = reader["DateStatus6"].ToString(),
                StatusEmailSale = reader["StatusEmailSale"].ToString(),
                StatusEmailShipping = reader["StatusEmailShipping"].ToString(),
                StatusEmailDelivery = reader["StatusEmailDelivery"].ToString(),
                StatusPoll = reader["StatusPoll"].ToString(),
                surveyResult = reader["surveyResult"].ToString(),
                country = reader["country"].ToString(),
                ActualDeliveryDate = reader["ActualDeliveryDate"].ToString(),
                cdnUrl = reader["cdnUrl"].ToString(),
                frame = reader["frame"].ToString(),
                edge = reader["edge"].ToString(),
                editUrl = reader["editUrl"].ToString(),
                idShippingUser = (int)reader["idShippingUser"],

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

                    DataShippingUser Userbuyer = new DataShippingUser();
                    using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailShippingUserByOrder", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pid", use.idOrder.Split(',')[i].ToString()));
                        //await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                Userbuyer.idOrder = (int)reader["idOrder"];
                                Userbuyer.email = reader["email"].ToString();
                                Userbuyer.Fullname = reader["Fullname"].ToString();
                                Userbuyer.NameStatus = reader["NameStatus"].ToString();
                                Userbuyer.idShippingUser = (int)reader["idShippingUser"];
                                Userbuyer.present = reader["present"].ToString();
                            }

                        }
                    }

                    DataEmailAddressUser UserAddress = new DataEmailAddressUser();
                    using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailAddressUser", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@pidOrder", use.idOrder.Split(',')[i].ToString()));
                        cmd.Parameters.Add(new SqlParameter("@pidShippingUser", Userbuyer.idShippingUser));

                        //await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                UserAddress.FullName = reader["FullName"].ToString();
                                UserAddress.email = reader["email"].ToString();
                            }
                        }
                    }

                    List<DataEmailAdmins> EmailAdmins = new List<DataEmailAdmins>();
                    using (SqlCommand cmd = new SqlCommand("SP_GetEmailAdmins", sql))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        //await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                EmailAdmins.Add(new DataEmailAdmins
                                {
                                    FullName = reader["FullName"].ToString()
                                    ,
                                    email = reader["email"].ToString()
                                });
                            }

                        }
                    }

                    string Cuerpomail = "";
                    EnviarEMail em = new EnviarEMail();

                    if (Userbuyer.present == "true")
                    {
                        if (use.idStatus == 6)
                        {
                            Cuerpomail = em.cambioEstadoProducto6(Userbuyer.Fullname, UserAddress.FullName);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Cambio de estado en tu producto", Userbuyer.email);
                        }
                        else if (use.idStatus == 7)
                        {
                            Cuerpomail = em.cambioEstadoProducto7(Userbuyer.Fullname, UserAddress.FullName);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Cambio de estado en tu producto", UserAddress.email);
                            Cuerpomail = em.cuerpoEntregaComprador(Userbuyer.Fullname, UserAddress.FullName);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Confirmación de cuadroteca entregada comprador", Userbuyer.email);
                            Cuerpomail = em.emailSeguimiento7(Userbuyer.Fullname);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "correo de seguimiento en la cuadroteca", Userbuyer.email);
                        }
                    }
                    else
                    {
                        if (use.idStatus == 6)
                        {
                            Cuerpomail = em.cuerpoDespachoCuadrotecaPropia(Userbuyer.Fullname, UserAddress.FullName);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Cambio de estado en tu producto", Userbuyer.email);
                        }
                        else if(use.idStatus == 7)
                        {
                            Cuerpomail = em.CuerpoCuadrotecaPropiaEntregado(Userbuyer.Fullname, UserAddress.FullName); 
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Cambio de estado en tu producto", Userbuyer.email);
                            Cuerpomail = em.emailSeguimiento7(Userbuyer.Fullname);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "correo de seguimiento en la cuadroteca", Userbuyer.email);
                        }
                    }
                    foreach (DataEmailAdmins row2 in EmailAdmins)
                    {
                        Cuerpomail = em.CambioEstadocuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                        em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado un cambio de estado en un producto en la plataforma", row2.email);
                    }
                }
            }

           
            return;

        }

        public async Task UpdateInvoiceOrder(UpdateInvoiceOrderRequeride use)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateInvoiceOrder", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pinvoice", use.invoice));
                    cmd.Parameters.Add(new SqlParameter("@pidOrder", use.idOrder));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;

                }
            }
        }
    }
}

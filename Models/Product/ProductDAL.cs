using fotoTeca.Data;
using fotoTeca.Email;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.Product
{
    // SDK de Mercado Pago
    // Agrega credenciales
    public class ProductDAL
    {
        private readonly string _connectionStrings;

        public ProductDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }
        public async Task<object> StoreOrder(ProductOrderRequeride req)
        {
            //ESTE ES PARA COLOMBIA
            MercadoPagoConfig.AccessToken = "TEST-1895584185781642-062123-477651f1c560f23c8523e01b9af134b6-777110226";
            //ESTOS PARA CHILE PRUEBAS Y PRODUCION
            //MercadoPagoConfig.AccessToken = "APP_USR-4095435028365401-021019-80f1ff21bf22d77f1063ea7fab338ee5-1000181481";
            //MercadoPagoConfig.AccessToken = "TEST-4095435028365401-021019-bba5cc6658a599f942ebf775a0587236-1000181481";


            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                Preference preference;

                OrderResponse resp = new OrderResponse();



                using (SqlCommand cmd = new SqlCommand("SP_StoreOrder", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidShippingUser", req.idShippingUser));
                    cmd.Parameters.Add(new SqlParameter("@pidProduct", req.idProduct));
                    cmd.Parameters.Add(new SqlParameter("@pTitle", req.title));
                    cmd.Parameters.Add(new SqlParameter("@pQuantity", req.quantity));
                    cmd.Parameters.Add(new SqlParameter("@pidcurrency", req.idcurrency));
                    cmd.Parameters.Add(new SqlParameter("@pUnitPrice", req.unitPrice));
                    cmd.Parameters.Add(new SqlParameter("@uuid", req.uuid));
                    cmd.Parameters.Add(new SqlParameter("@cdnUrl", req.cdnUrl));
                    cmd.Parameters.Add(new SqlParameter("@frame", req.frame));
                    cmd.Parameters.Add(new SqlParameter("@edge", req.edge));
                    cmd.Parameters.Add(new SqlParameter("@pcountry", req.country));
                    cmd.Parameters.Add(new SqlParameter("@idPromotion", req.idPromotion));
                    cmd.Parameters.Add(new SqlParameter("@pidGiftCardUsedForPurchase", req.idGiftCardUsedForPurchase));
                    cmd.Parameters.Add(new SqlParameter("@codeGiftCard", req.CodeGiftCardUsedForPurchase));
                    cmd.Parameters.Add(new SqlParameter("@pTotalPrice", req.totalPrice));
                    cmd.Parameters.Add(new SqlParameter("@pEditUrl", req.editUrl));
                    cmd.Parameters.Add(new SqlParameter("@pBalanceTemporary", req.balanceTemporary));
                    cmd.Parameters.Add(new SqlParameter("@pIva", req.iva));
                    cmd.Parameters.Add(new SqlParameter("@pPriceShipment", req.priceShipment));
                    cmd.Parameters.Add(new SqlParameter("@pPresent", req.present));

                    //List<string> ListIdproduct = req.idProduct.Split(',').ToList();
                    //List<string> ListTitle = req.title.Split(',').ToList();
                    //List<string> ListQuantity = req.quantity.Split(',').ToList();
                    //List<string> ListUnitprice = req.unitPrice.Split(',').ToList();

                    //IList<PreferenceItemRequest> preferenceItemRequests = new List<PreferenceItemRequest>();
                    //var cantidad = ListIdproduct.Count();
                    //double PrecioTotal = req.totalPrice / cantidad;
                    //int cerrado = (int)Math.Round(PrecioTotal, MidpointRounding.AwayFromZero);

                    //for (int i = 0; i < ListIdproduct.Count; i++) {
                    //    preferenceItemRequests.Add(new PreferenceItemRequest
                    //    {
                    //        Title = ListTitle[i],
                    //        Quantity = Convert.ToInt32(ListQuantity[i]),
                    //        CurrencyId ="COL",
                    //        UnitPrice = Convert.ToInt32(cerrado),
                    //    });
                    //}
                    //var request = new PreferenceRequest
                    //{
                    //    Items = new List<PreferenceItemRequest>(preferenceItemRequests)
                    //    {

                    //    },
                    //    BackUrls = new PreferenceBackUrlsRequest
                    //    {
                    //        Success = "http://localhost:4200/#/home-colombia/after-checkout",
                    //        Failure = "http://localhost:4200/#/home-colombia",
                    //        Pending = "http://localhost:4200/#/home-colombia",
                    //    },
                    //    AutoReturn = "approved",
                    //};


                    if (req.totalPrice == 0)
                    {
                        await sql.OpenAsync();
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                resp.idOrder = (int)reader["vidOrder"];
                            }
                        }

                        cmd.Parameters.Add(new SqlParameter("@pId", "Compra con descuento completo de la giftcard"));


                        DataShippingUser Userbuyer = new DataShippingUser();
                        using (SqlCommand cmd2 = new SqlCommand("SP_GetDataEmailShippingUserByOrder", sql))
                        {
                            cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd2.Parameters.Add(new SqlParameter("@pid", resp.idOrder));
                            //await sql.OpenAsync();
                            using (var reader = await cmd2.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    Userbuyer.idOrder = (int)reader["idOrder"];
                                    Userbuyer.idShippingUser = (int)reader["idShippingUser"];
                                    Userbuyer.email = reader["email"].ToString();
                                    Userbuyer.Fullname = reader["Fullname"].ToString();
                                    Userbuyer.NameStatus = reader["NameStatus"].ToString();
                                    Userbuyer.present = reader["present"].ToString();
                                }
                            }
                        }
                        DataEmailAddressUser UserAddress = new DataEmailAddressUser();
                        using (SqlCommand cmd3 = new SqlCommand("SP_GetDataEmailAddressUser", sql))
                        {
                            cmd3.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd3.Parameters.Add(new SqlParameter("@pidOrder", resp.idOrder));
                            cmd3.Parameters.Add(new SqlParameter("@pidShippingUser", req.idShippingUser));

                            //await sql.OpenAsync();
                            using (var reader = await cmd3.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    UserAddress.FullName = reader["FullName"].ToString();
                                    UserAddress.email = reader["email"].ToString();
                                }
                            }
                        }

                        List<DataEmailAdmins> EmailAdmins = new List<DataEmailAdmins>();
                        using (SqlCommand cmd4 = new SqlCommand("SP_GetEmailAdmins", sql))
                        {
                            cmd4.CommandType = System.Data.CommandType.StoredProcedure;

                            //await sql.OpenAsync();
                            using (var reader = await cmd4.ExecuteReaderAsync())
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
                        StatusResponse respS = new StatusResponse();
                        using (SqlCommand cmd5 = new SqlCommand("SP_UpdateStatusOrder", sql))
                        {
                            cmd5.CommandType = System.Data.CommandType.StoredProcedure;
                  
                            cmd5.Parameters.Add(new SqlParameter("@Status", 3));
                            cmd5.Parameters.Add(new SqlParameter("@pIdOrder", resp.idOrder));
                            cmd5.Parameters.Add(new SqlParameter("@pBalance", req.balanceTemporary));
                            cmd5.Parameters.Add(new SqlParameter("@pCode", req.CodeGiftCardUsedForPurchase));

                            //await sql.OpenAsync();
                            using (var reader = await cmd5.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    respS.idGiftCard = (int)reader["idGiftCard"];
                                    respS.codeGitfCard = reader["codeGitfCard"].ToString();
                        


                                }
                            }
                        }
                        string Cuerpomail = "";
                        EnviarEMail em = new EnviarEMail();

                        if (Userbuyer.present == "true")
                        {
                            Cuerpomail = em.cuerpoCompradorRegalo(Userbuyer.Fullname, UserAddress.FullName);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Confirmación de compra de tu Cuadroteca", Userbuyer.email);
                        }
                        else if (Userbuyer.present == "false")
                        {
                            Cuerpomail = em.cuerpoComprador(Userbuyer.Fullname, Userbuyer.NameStatus);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Confirmación de compra de tu Cuadroteca", Userbuyer.email);
                        }

                        foreach (DataEmailAdmins row2 in EmailAdmins)
                        {
                            Cuerpomail = em.cuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado una nueva compra en la plataforma", row2.email);
                        }

                        directpaymentResponse resp2 = new directpaymentResponse();

                        resp2.url = "after-checkout";

                        return resp2;


                       

                    }

                    else {

                        var request = new PreferenceRequest
                        {
                            Items = new List<PreferenceItemRequest>
                        {
                            new PreferenceItemRequest
                                {
                                    Title = "cuadro",
                                    Quantity = 1,
                                    //CurrencyId = "CLP",
                                    CurrencyId = "COP",
                                    UnitPrice = req.totalPrice,
                                },
                        },
                            BackUrls = new PreferenceBackUrlsRequest
                            {
                                Success = "https://fototecapgweb.azurewebsites.net/#/after-checkout",
                                Failure = "https://fototecapgweb.azurewebsites.net/#/after-checkout",
                                Pending = "https://fototecapgweb.azurewebsites.net/#/after-checkout",
                                //Success = "https://lacuadrotecachile.azurewebsites.net/#/after-checkout",
                                //Failure = "https://lacuadrotecachile.azurewebsites.net/#/after-checkout",
                                //Pending = "https://lacuadrotecachile.azurewebsites.net/#/after-checkout",
                            },

                            AutoReturn = "approved",
                        };

                        // Crea la preferencia usando el client
                        var client = new PreferenceClient();
                        preference = await client.CreateAsync(request);

                        cmd.Parameters.Add(new SqlParameter("@pId", preference.Id));


                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();

                        return preference;
                    }

                }
                //DataShippingUser Userbuyer = new DataShippingUser();
                //using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailShippingUser", sql))
                //{
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    cmd.Parameters.Add(new SqlParameter("@pid", preference.Id));
                //    //await sql.OpenAsync();
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            Userbuyer.idOrder = (int)reader["idOrder"];
                //            Userbuyer.idShippingUser = (int)reader["idShippingUser"];
                //            Userbuyer.email = reader["email"].ToString();
                //            Userbuyer.Fullname = reader["Fullname"].ToString();
                //            Userbuyer.NameStatus = reader["NameStatus"].ToString();
                //            Userbuyer.present = reader["present"].ToString();
                //        }
                //    }
                //}
                //DataEmailAddressUser UserAddress = new DataEmailAddressUser();
                //using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailAddressUser", sql))
                //{
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    cmd.Parameters.Add(new SqlParameter("@pid", preference.Id));
                //    cmd.Parameters.Add(new SqlParameter("@pidShippingUser", Userbuyer.idShippingUser));

                //    //await sql.OpenAsync();
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            UserAddress.FullName = reader["FullName"].ToString();
                //            UserAddress.email = reader["email"].ToString();
                //        }
                //    }
                //}

                //List<DataEmailAdmins> EmailAdmins = new List<DataEmailAdmins>();
                //using (SqlCommand cmd = new SqlCommand("SP_GetEmailAdmins", sql))
                //{
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //    //await sql.OpenAsync();
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            EmailAdmins.Add(new DataEmailAdmins
                //            {
                //                FullName = reader["FullName"].ToString()
                //                ,
                //                email = reader["email"].ToString()
                //            });
                //        }

                //    }
                //}
                //string Cuerpomail = "";
                //EnviarEMail em = new EnviarEMail();

                //if (Userbuyer.present == "true")
                //{
                //    Cuerpomail = em.cuerpoCompradorRegalo(Userbuyer.Fullname, UserAddress.FullName);
                //    em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Informacion sobre tu nueva compra", Userbuyer.email);
                //}
                //else if (Userbuyer.present == "false")
                //{
                //    Cuerpomail = em.cuerpoComprador(Userbuyer.Fullname, Userbuyer.NameStatus);
                //    em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Informacion sobre tu nueva compra", Userbuyer.email);
                //}

                //foreach (DataEmailAdmins row2 in EmailAdmins)
                //{
                //    Cuerpomail = em.cuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                //    em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado una nueva compra en la plataforma", row2.email);
                //}




            }
        }

        public async Task<Preference> StoreOrderGiftCard(ProductOrderRequeride2 req)
        {
            MercadoPagoConfig.AccessToken = "TEST-1895584185781642-062123-477651f1c560f23c8523e01b9af134b6-777110226";
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                CredencialesDeAcceso acceso = new CredencialesDeAcceso();
                Preference preference;
                acceso.creacionContrasena();
                string Code = acceso.creacionContrasena();

                using (SqlCommand cmd = new SqlCommand("SP_StoreOrderGiftCard", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pidShippingUser", req.idShippingUser));
                    cmd.Parameters.Add(new SqlParameter("@pidcurrency", req.idcurrency));
                    cmd.Parameters.Add(new SqlParameter("@pcountry", req.country));
                    cmd.Parameters.Add(new SqlParameter("@idPromotion", req.idPromotion));
                    cmd.Parameters.Add(new SqlParameter("@pidGiftCard", req.idGiftCard));
                    cmd.Parameters.Add(new SqlParameter("@pTotalPrice", req.totalPrice));
                    cmd.Parameters.Add(new SqlParameter("@pCodeGitfCard", Code));


                    //List<string> ListIdproduct = req.idGiftCard.Split(',').ToList();
                    //List<string> ListTitle = req.country.Split(',').ToList();
                    //List<string> ListQuantity = req.idGiftCard.Split(',').ToList();
                    //List<string> ListUnitprice = req.totalPrice.ToString().Split(',').ToList();
                    //IList<PreferenceItemRequest> preferenceItemRequests = new List<PreferenceItemRequest>();
                    //for (int i = 0; i < ListIdproduct.Count; i++)
                    //{
                    //    preferenceItemRequests.Add(new PreferenceItemRequest
                    //    {
                    //        Title = ListTitle[i],
                    //        Quantity = Convert.ToInt32(ListQuantity[i]),
                    //        CurrencyId = "COL",
                    //        //CurrencyId = req.idcurrency.Split(',')[0].ToString(),
                    //        UnitPrice = Convert.ToInt32(ListUnitprice[i]),
                    //    });
                    //}

                    var request = new PreferenceRequest
                    {
                        Items = new List<PreferenceItemRequest>
                        {
                            new PreferenceItemRequest
                                {
                                    Title = "GiftCard",
                                    Quantity = 1,
                                    //CurrencyId = "COP",
                                    CurrencyId = "CLP",
                                    UnitPrice = req.totalPrice,
                                },
                        },
                        BackUrls = new PreferenceBackUrlsRequest
                        {
                            Success = "https://fototecapgweb.azurewebsites.net/#/after-checkout",
                            Failure = "https://fototecapgweb.azurewebsites.net/#/after-checkout",
                            Pending = "https://fototecapgweb.azurewebsites.net/#/after-checkout",
                            //Success = "https://lacuadrotecachile.azurewebsites.net/#/after-checkout",
                            //Failure = "https://lacuadrotecachile.azurewebsites.net/#/after-checkout",
                            //Pending = "https://lacuadrotecachile.azurewebsites.net/#/after-checkout",

                        }
                    };
                    // Crea la preferencia usando el client
                    var client = new PreferenceClient();
                    preference = await client.CreateAsync(request);

                    cmd.Parameters.Add(new SqlParameter("@pId", preference.Id));

                    await sql.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();

                }

                //DataShippingUser Userbuyer = new DataShippingUser();
                //using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailShippingUser", sql))
                //{
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                //    cmd.Parameters.Add(new SqlParameter("@pid", preference.Id));
                //    //await sql.OpenAsync();
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            Userbuyer.idOrder = (int)reader["idOrder"];
                //            Userbuyer.email = reader["email"].ToString();
                //            Userbuyer.Fullname = reader["Fullname"].ToString();
                //            Userbuyer.NameStatus = reader["NameStatus"].ToString();

                //        }

                //    }
                //}

                //List<DataEmailAdmins> EmailAdmins = new List<DataEmailAdmins>();
                //using (SqlCommand cmd = new SqlCommand("SP_GetEmailAdmins", sql))
                //{
                //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                //    //await sql.OpenAsync();
                //    using (var reader = await cmd.ExecuteReaderAsync())
                //    {
                //        while (await reader.ReadAsync())
                //        {
                //            EmailAdmins.Add(new DataEmailAdmins
                //            {
                //                FullName = reader["FullName"].ToString()
                //                ,
                //                email = reader["email"].ToString()
                //            });
                //        }

                //    }
                //}
                //string Cuerpomail = "";
                //EnviarEMail em = new EnviarEMail();
                //Cuerpomail = em.cuerpoCompradorGitcard(Userbuyer.Fullname, Userbuyer.NameStatus);
                //em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Tu compra ha sido exitosa", Userbuyer.email);

                //foreach (DataEmailAdmins row2 in EmailAdmins)
                //{
                //    Cuerpomail = em.cuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                //    em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado una nueva compra en la plataforma", row2.email);
                //}
                return preference;



            }
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

            //using (var reader = await cmd.ExecuteReaderAsync())
            //{
            //    while (await reader.ReadAsync())
            //    {
            //        response.Add(
            //        new Models.Usuarios()
            //        {
            //            Id = Convert.ToInt32(reader["id"]),
            //            Nombre = (string)reader["nombre"],
            //            Username = (string)reader["username"],
            //            FechaCreacion = (DateTime)reader["fechaCreacion"]
            //        });
            //    }

            //}

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

        public async Task<StatusResponse> UpdateStatusOrder(StatusRequride use)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                StatusResponse resp = new StatusResponse();

                using (SqlCommand cmd = new SqlCommand("SP_UpdateStatusOrder", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if(use.Status == "pending") {
                    cmd.Parameters.Add(new SqlParameter("@Status",1));
                    }
                    else if(use.Status == "reject"){ 
                    cmd.Parameters.Add(new SqlParameter("@Status",2));
                    }
                    else if (use.Status == "approved"){
                        cmd.Parameters.Add(new SqlParameter("@Status",3));
                    }
                    cmd.Parameters.Add(new SqlParameter("@pIdMercadoPago", use.IdMercadoPag));
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resp.idGiftCard = (int)reader["idGiftCard"];
                            resp.codeGitfCard = reader["codeGitfCard"].ToString();

                        }
                    }
                }
                DataShippingUser Userbuyer = new DataShippingUser();
                using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailShippingUser", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pid", use.IdMercadoPag));
                    //await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            Userbuyer.idOrder = (int)reader["idOrder"];
                            Userbuyer.idShippingUser = (int)reader["idShippingUser"];
                            Userbuyer.email = reader["email"].ToString();
                            Userbuyer.Fullname = reader["Fullname"].ToString();
                            Userbuyer.NameStatus = reader["NameStatus"].ToString();
                            Userbuyer.present = reader["present"].ToString();
                        }
                    }
                }
                DataEmailAddressUser UserAddress = new DataEmailAddressUser();
                using (SqlCommand cmd = new SqlCommand("SP_GetDataEmailAddressUser", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pid", use.IdMercadoPag));
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

                if (use.Status == "approved")
                {
                    if (resp.idGiftCard > 0)
                    {

                        Cuerpomail = em.cuerpoCompraGiftCard(Userbuyer.Fullname, UserAddress.FullName);
                        em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Giftcard enviada", Userbuyer.email);

                        Cuerpomail = em.cuerpoDestinatarioGitcard(Userbuyer.Fullname, UserAddress.FullName, resp.codeGitfCard);
                        em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Has recibido una Giftcard de alguien especial", UserAddress.email);

                        foreach (DataEmailAdmins row2 in EmailAdmins)
                        {
                            Cuerpomail = em.cuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado una nueva compra en la plataforma", row2.email);
                        }

                        List<GitfCard> nada = new List<GitfCard>();
                        using (SqlCommand cmd = new SqlCommand("SP_UpdateStatusGiftcard", sql))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@pidOrder", Userbuyer.idOrder));
                            //await sql.OpenAsync();
                            using (var reader = await cmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    nada.Add(new GitfCard
                                    {
                                        idOrder = reader["idOrder"].ToString()
                                    });
                                }
                            }
                        }
                    }
                    else {

                        if (Userbuyer.present == "true")
                        {
                            Cuerpomail = em.cuerpoCompradorRegalo(Userbuyer.Fullname, UserAddress.FullName);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Has recibido una cuadroteca de regalo", Userbuyer.email);
                        }
                        else if (Userbuyer.present == "false")
                        {
                            Cuerpomail = em.cuerpoComprador(Userbuyer.Fullname, Userbuyer.NameStatus);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Confirmación de compra de tu Cuadroteca", Userbuyer.email);
                        }
                        foreach (DataEmailAdmins row2 in EmailAdmins)
                        {
                            Cuerpomail = em.cuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                            em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado una nueva compra en la plataforma", row2.email);
                        }
                    }
                }
                return resp;
            }
        }

    }
}

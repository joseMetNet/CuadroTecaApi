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
        public async Task<Preference> StoreOrder(ProductOrderRequeride req
            
            )
        {
            MercadoPagoConfig.AccessToken = "TEST-1895584185781642-062123-477651f1c560f23c8523e01b9af134b6-777110226";
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                Preference preference;
                using (SqlCommand cmd = new SqlCommand("SP_StoreOrder", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.Add(new SqlParameter("@pidShippingUser", req.idShippingUser));
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

                    List<string> ListIdproduct = req.idProduct.Split(',').ToList();
                    List<string> ListTitle = req.title.Split(',').ToList();
                    List<string> ListQuantity = req.quantity.Split(',').ToList();
                    List<string> ListUnitprice = req.unitPrice.Split(',').ToList();
                    IList<PreferenceItemRequest> preferenceItemRequests = new List<PreferenceItemRequest>();
                    for (int i = 0; i < ListIdproduct.Count; i++) {
                        preferenceItemRequests.Add(new PreferenceItemRequest
                        {
                            Title = ListTitle[i],
                            Quantity = Convert.ToInt32(ListQuantity[i]),
                            CurrencyId ="COL",
                            //CurrencyId = req.idcurrency.Split(',')[0].ToString(),
                            UnitPrice = Convert.ToInt32(ListUnitprice[i]),
                        });
                    }
                    var request = new PreferenceRequest
                    {
                        Items = new List<PreferenceItemRequest>(preferenceItemRequests)
                        {
                         
                        },
                        BackUrls = new PreferenceBackUrlsRequest
                        {
                            Success = "http://localhost:4200/#/home-colombia",
                            Failure = "http://localhost:4200/#/home-colombia",
                            Pending = "http://localhost:4200/#/home-colombia",
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
                //Cuerpomail = em.cuerpoComprador(Userbuyer.Fullname, Userbuyer.NameStatus);
                //em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Tu compra ha sido exitosa", Userbuyer.email);

                //foreach (DataEmailAdmins row2 in EmailAdmins)
                //{
                //    Cuerpomail = em.cuerpoAdmins(row2.FullName, Userbuyer.idOrder, Userbuyer.NameStatus);
                //    em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(), "Han realizado una nueva compra en la plataforma", row2.email);
                //}




                return preference;

              
            }
        }


        public async Task UpdateStatusOrder(StatusRequride use)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))

            {
                using (SqlCommand cmd = new SqlCommand("SP_UpdateStatusOrder", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Status", use.Status));
                    cmd.Parameters.Add(new SqlParameter("@pIdMercadoPago", use.IdMercadoPag));
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    return;
                }
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

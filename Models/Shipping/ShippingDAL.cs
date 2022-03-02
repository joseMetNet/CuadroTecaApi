using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Models.ShippingUser
{
    public class ShippingDAL
    {
        private readonly string _connectionStrings;


        public ShippingDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");
        }

        public async Task<List<ShippingUserResponse>> storeOrUpdateShippingUser(ShippingUserRequeride product)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateShippingUser", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (product.idShippingUser != 0) cmd.Parameters.Add(new SqlParameter("@pidShippingUser", product.idShippingUser));
                    cmd.Parameters.Add(new SqlParameter("@pName", product.Name));
                    cmd.Parameters.Add(new SqlParameter("@pLastName", product.lastName));
                    cmd.Parameters.Add(new SqlParameter("@pTypeHome", product.typeHome));
                    cmd.Parameters.Add(new SqlParameter("@pAddress", product.address));
                    cmd.Parameters.Add(new SqlParameter("@pidCity", product.pidCity));
                    cmd.Parameters.Add(new SqlParameter("@pidDepartment", product.pidDepartment));
                    cmd.Parameters.Add(new SqlParameter("@pPhone", product.phone));
                    cmd.Parameters.Add(new SqlParameter("@pEmail", product.email));
                    cmd.Parameters.Add(new SqlParameter("@pidProduct", product.idProduct));
                    cmd.Parameters.Add(new SqlParameter("@pidGiftCard", product.idGiftCard));
                    cmd.Parameters.Add(new SqlParameter("@pNit", product.Nit));

                    //string[] arreglo = product.NameCity.Split(',');
                    //for (int i = 0; i < arreglo.Length; i++)
                    //{

                    //}

                    var response = new List<ShippingUserResponse>();
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

        public ShippingUserResponse MapToValue1(SqlDataReader reader)
        {
            return new ShippingUserResponse()
            {
                api_dice = reader["api_dice"].ToString(),
                idShippingUser = (int)reader["idShippingUser"],


            };
        }
        public async Task<List<ShippingAddressResponse>> storeOrUpdateShippingAddress(ShippingAddressRequeride product)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateShippingAddress", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (product.idShippingAddress != 0) cmd.Parameters.Add(new SqlParameter("@pidShippingAddress", product.idShippingAddress));
                    cmd.Parameters.Add(new SqlParameter("@pName", product.Name));
                    cmd.Parameters.Add(new SqlParameter("@pLastName", product.lastName));
                    cmd.Parameters.Add(new SqlParameter("@pAddress", product.address));
                    cmd.Parameters.Add(new SqlParameter("@pidCity", product.idCity));
                    cmd.Parameters.Add(new SqlParameter("@pidDepartment", product.idDepartment));
                    cmd.Parameters.Add(new SqlParameter("@pEmail", product.email));
                    cmd.Parameters.Add(new SqlParameter("@pPhone", product.phone));
                    cmd.Parameters.Add(new SqlParameter("@pidTypeClient", product.idTypeClient));
                    cmd.Parameters.Add(new SqlParameter("@pNit", product.nit));
                    cmd.Parameters.Add(new SqlParameter("@pidShippingUser", product.idShippingUser));


                    //string[] arreglo = product.NameCity.Split(',');
                    //for (int i = 0; i < arreglo.Length; i++)
                    //{

                    //}

                    var response = new List<ShippingAddressResponse>();
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

        public ShippingAddressResponse MapToValue2(SqlDataReader reader)
        {
            return new ShippingAddressResponse()
            {
                api_dice = reader["api_dice"].ToString(),
                idShippingAddress = (int)reader["idShippingAddress"],


            };
        }

        //public async Task<List<storeOrUpdateShippingUserGiftCardResponse>> storeOrUpdateShippingUserGiftCard(ShippingUserGiftCardRequeride product)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionStrings))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateShippingUserGiftCard", sql))
        //        {

        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            if (product.idShippingUserGiftCard != 0) cmd.Parameters.Add(new SqlParameter("@pidShippingUserGiftCard", product.idShippingUserGiftCard));
        //            cmd.Parameters.Add(new SqlParameter("@pName", product.Name));
        //            cmd.Parameters.Add(new SqlParameter("@pLastName", product.lastName));
        //            cmd.Parameters.Add(new SqlParameter("@pTypeHome", product.typeHome));
        //            cmd.Parameters.Add(new SqlParameter("@pAddress", product.address));
        //            cmd.Parameters.Add(new SqlParameter("@pidCity", product.idCity));
        //            cmd.Parameters.Add(new SqlParameter("@pidDepartment", product.idDepartment));
        //            cmd.Parameters.Add(new SqlParameter("@pEmail", product.email));
        //            cmd.Parameters.Add(new SqlParameter("@pidGiftCard", product.idGiftCard));
        
        //            var response = new List<storeOrUpdateShippingUserGiftCardResponse>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValue4(reader));
        //                }

        //            }

        //            return response;
        //        }

        //    }

        //}

        //public storeOrUpdateShippingUserGiftCardResponse MapToValue4(SqlDataReader reader)
        //{
        //    return new storeOrUpdateShippingUserGiftCardResponse()
        //    {
        //        api_dice = reader["api_dice"].ToString(),
        //        idShippingUserGiftCard = (int)reader["idShippingUserGiftCard"],


        //    };
        //}


        //public async Task<List<ShippingAddressGiftCardResponse>> storeOrUpdateShippingAddressGiftCard(ShippingAddressGiftCardRequeride product)
        //{
        //    using (SqlConnection sql = new SqlConnection(_connectionStrings))
        //    {
        //        using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateShippingAddressGiftCard", sql))
        //        {

        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            if (product.idShippingUserGiftCard != 0) cmd.Parameters.Add(new SqlParameter("@pidShippingAddressGiftCard", product.idShippingAddressGiftCard));
        //            cmd.Parameters.Add(new SqlParameter("@pName", product.Name));
        //            cmd.Parameters.Add(new SqlParameter("@pLastName", product.lastName));
        //            cmd.Parameters.Add(new SqlParameter("@pEmail", product.email));
        //            cmd.Parameters.Add(new SqlParameter("@pidShippingUserGiftCard", product.idShippingUserGiftCard));
   
        //            var response = new List<ShippingAddressGiftCardResponse>();
        //            await sql.OpenAsync();

        //            using (var reader = await cmd.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    response.Add(MapToValue5(reader));
        //                }

        //            }

        //            return response;
        //        }

        //    }

        //}

        //public ShippingAddressGiftCardResponse MapToValue5(SqlDataReader reader)
        //{
        //    return new ShippingAddressGiftCardResponse()
        //    {
        //        api_dice = reader["api_dice"].ToString(),
        //        idShippingAddressGiftCard = (int)reader["idShippingAddressGiftCard"],

        //    };
        //}

        public async Task<List<ShippingUserResponse2>> GetShippingUser(int idShippingUser = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetShippingUser", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (idShippingUser != 0) cmd.Parameters.Add(new SqlParameter("@pidShippingUser", idShippingUser));


                    var response = new List<ShippingUserResponse2>();
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

        public ShippingUserResponse2 MapToValue3(SqlDataReader reader)
        {
            return new ShippingUserResponse2()
            {
                idShippingUser = (int)reader["idShippingUser"],
                Name = reader["Name"].ToString(),
                lastName = reader["lastName"].ToString(),
                typeHome = reader["typeHome"].ToString(),
                address = reader["address"].ToString(),
                idCity = (int)reader["idCity"],
                City = reader["City"].ToString(),
                idDepartment = (int)reader["idDepartment"],
                nameDepartment = reader["nameDepartment"].ToString(),
                phone = reader["phone"].ToString(),
                datetime = reader["datetime"].ToString(),
                idProduct = (int)reader["idProduct"],
                email = reader["email"].ToString(),
                Nit = reader["Nit"].ToString(),

            };
        }
    }
}

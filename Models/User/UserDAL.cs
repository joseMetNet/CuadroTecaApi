using fotoTeca.Data;
using fotoTeca.Email;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace fotoTeca.Models.User
{
    public class UserDAL
    {
        private readonly string _connectionStrings;
        CredencialesDeAcceso acceso = new CredencialesDeAcceso();

        public UserDAL(IConfiguration configuration/*, IDataProtectionProvider protectionProvider*/)
        {
            _connectionStrings = configuration.GetConnectionString("DefaultConnection");
            //_protector = protectionProvider.CreateProtector("nnn");

        }
       
          public async Task<RecoverPassworResponse> UpdatePasswordUser(RecoverPassworRequeride password)
          {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                RecoverPassworResponse pass = new RecoverPassworResponse();
                CredencialesDeAcceso acceso = new CredencialesDeAcceso();
                acceso.creacionContrasena();
                RijndaelManaged myRijndael = new RijndaelManaged();
                myRijndael.GenerateKey();
                myRijndael.GenerateIV();
                string passNew = acceso.creacionContrasena();

                using (SqlCommand cmd = new SqlCommand("SP_UpdatePasswordUser", sql))
                {
                    Byte[] contrasenaEncriptada = acceso.EncryptStringToBytes(passNew, myRijndael.Key, myRijndael.IV);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@pEmail", password.email));
                    cmd.Parameters.Add(new SqlParameter("@pPassword", contrasenaEncriptada));
                    cmd.Parameters.Add(new SqlParameter("@pkey", myRijndael.Key));
                    cmd.Parameters.Add(new SqlParameter("@pIv", myRijndael.IV));
                    //string textocifrado = _protector.Unprotect(user.password);

                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            pass.api_dice = reader["api_dice"].ToString();
                            pass.idUser = (int)reader["idUser"];
                            pass.firstName = reader["firstName"].ToString();
                            pass.lastName = reader["lastName"].ToString();
                            pass.idTypeUser = reader["idTypeUser"].ToString();
                            pass.email = reader["email"].ToString();
                            pass.idStatusUser = reader["idStatusUser"].ToString();

                        }
                    }
                   
                }
                string Cuerpomail = "";
                if (pass.idUser > 0)
                {
                EnviarEMail em = new EnviarEMail();
                Cuerpomail = em.cuerpoRecuperarContraseña(pass.firstName, passNew);
                em.EnviarCorreo(Cuerpomail, _connectionStrings.ToString(),"Cambio de contraseña", pass.email);
                }

                return pass;
            }
        }
        public async Task<List<UserResponse>> StoreOrUpdateUser(UserDTOrequired user)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_storeOrUpdateUser", sql))
                {

                    CredencialesDeAcceso acceso = new CredencialesDeAcceso();
                    //string contrasena = acceso.creacionContrasena();
                    RijndaelManaged myRijndael = new RijndaelManaged();
                    myRijndael.GenerateKey();
                    myRijndael.GenerateIV();
                    Byte[] contrasenaEncriptada = acceso.EncryptStringToBytes(user.password, myRijndael.Key, myRijndael.IV);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (user.idUser != 0) cmd.Parameters.Add(new SqlParameter("@pidUser", user.idUser));
                    cmd.Parameters.Add(new SqlParameter("@pFirstName", user.firstName));
                    cmd.Parameters.Add(new SqlParameter("@pLastName", user.lastName));
                    cmd.Parameters.Add(new SqlParameter("@pidTypeUser", user.idTypeUser));
                    cmd.Parameters.Add(new SqlParameter("@pEmail", user.email));
                    //cmd.Parameters.Add(new SqlParameter("@pUserName", user.userName));

                    cmd.Parameters.Add(new SqlParameter("@pPassword", contrasenaEncriptada));
                    cmd.Parameters.Add(new SqlParameter("@pkey", myRijndael.Key));
                    cmd.Parameters.Add(new SqlParameter("@pIv", myRijndael.IV));

                    var response = new List<UserResponse>();
                    await sql.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Add(MapToValue0(reader));
                        }

                    }

                    return response;
                }

            }

        }

        public UserResponse MapToValue0(SqlDataReader reader)
        {
            return new UserResponse()
            {
                idUser = (int)reader["idUser"],
                firstName = reader["firstName"].ToString(),
                lastName = reader["lastName"].ToString(),
                idTypeUser = (int)reader["idTypeUser"],
                email = reader["email"].ToString(),
                idStatus = (int)reader["idStatus"]

            };
        }

        public async Task<List<UserResponse>> GetUsers(int pidUser = 0)
        {
            using (SqlConnection sql = new SqlConnection(_connectionStrings))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetUsers", sql))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    if (pidUser != 0) cmd.Parameters.Add(new SqlParameter("@pidUser", pidUser));

                    var response = new List<UserResponse>();
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

        public UserResponse MapToValue1(SqlDataReader reader)
        {
            return new UserResponse()
            {
                idUser = (int)reader["idUser"],
                firstName = reader["firstName"].ToString(),
                lastName = reader["lastName"].ToString(),
                idTypeUser = (int)reader["idTypeUser"],
                email = reader["email"].ToString(),
                idStatus = (int)reader["idStatus"]
            };
        }

        
    }
}

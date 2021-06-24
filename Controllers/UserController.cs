using fotoTeca.Models.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class UserController : ControllerBase
    {
        private readonly UserDAL _repository1;

        public UserController(UserDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }

        //POST

        [HttpPost("/StoreOrUpdateUser", Name = "StoreOrUpdateUser")]
        public async Task<List<UserResponse>> post1(UserDTOrequired user)
        {
            return await _repository1.StoreOrUpdateUser(user);
        }

        //[HttpPost("/UpdatePasswordUser", Name = "UpdatePasswordUser")]
        //public async Task post2([FromBody] RecoverPassworRequeride password)

        //{
        //    await _repository1.UpdatePasswordUser(password);
        //}

        [HttpPut("/UpdatePasswordUser", Name = "UpdatePasswordUser")]
        public async Task<RecoverPassworResponse> post2([FromBody] RecoverPassworRequeride password)

        {
            return await _repository1.UpdatePasswordUser(password);
        }
        //GET
        [HttpGet("/GetUsers")]
        public async Task<List<UserResponse>> get1()
        {
            return await _repository1.GetUsers();
        }

        [HttpGet("/GetUsers/{pidUser}")]
        public async Task<List<UserResponse>> get12(int pidUser )
        {
            return await _repository1.GetUsers(pidUser);
        }
        
    }
}

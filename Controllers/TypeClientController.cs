using fotoTeca.Models.TypeClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TypeClientController
    {
        private readonly TypeClientDAL _repository1;

        public TypeClientController(TypeClientDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }
        [HttpGet("/GetTypeClient")]
        public async Task<List<TypeClientResponse>> get1()
        {
            return await _repository1.GetTypeClient();
        }
    }
}

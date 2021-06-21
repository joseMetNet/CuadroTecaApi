using fotoTeca.Models.Platform;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace fotoTeca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StoreController : ControllerBase
    {
        private readonly StoreDAL _repository1;

        public StoreController(StoreDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }
        [HttpGet("/GetStore")]
        public async Task<List<StoreResponse>> get1()
        {
            return await _repository1.GetStore();
        }

        [HttpGet("/GetStore/{idStore}")]
        public async Task<List<StoreResponse>> get12(int idStore)
        {
            return await _repository1.GetStore(idStore);
        }

    }
}

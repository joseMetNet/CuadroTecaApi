using fotoTeca.Models.ShippingUser;
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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ShippingController : ControllerBase
    {
        private readonly ShippingDAL _repository1;

        public ShippingController (ShippingDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }
        [HttpPost("/storeOrUpdateShippingUser", Name = "storeOrUpdateShippingUser")]
        public async Task<List<ShippingUserResponse>> Post1(ShippingUserRequeride product)

        {
            return await _repository1.storeOrUpdateShippingUser(product);
        }
        [HttpPost("/storeOrUpdateShippingAddress", Name = "storeOrUpdateShippingAddress")]
        public async Task<List<ShippingAddressResponse>> Post2(ShippingAddressRequeride product)

        {
            return await _repository1.storeOrUpdateShippingAddress(product);
        }
        //[HttpPost("/storeOrUpdateShippingUserGiftCard", Name = "storeOrUpdateShippingUserGiftCard")]
        //public async Task<List<storeOrUpdateShippingUserGiftCardResponse>> Post3(ShippingUserGiftCardRequeride product)
        //{
        //    return await _repository1.storeOrUpdateShippingUserGiftCard(product);
        //}
        //[HttpPost("/storeOrUpdateShippingAddressGiftCard", Name = "storeOrUpdateShippingAddressGiftCard")]
        //public async Task<List<ShippingAddressGiftCardResponse>> Post4(ShippingAddressGiftCardRequeride product)
        //{
        //    return await _repository1.storeOrUpdateShippingAddressGiftCard(product);
        //}
        [HttpGet("/GetShippingUser")]
        public async Task<List<ShippingUserResponse2>> get1()
        {
            return await _repository1.GetShippingUser();

        }
        [HttpGet("/GetShippingUser/{idShippingUser}")]
        public async Task<List<ShippingUserResponse2>> get2(int idShippingUser)
        {
            return await _repository1.GetShippingUser(idShippingUser);

        }
    }
}

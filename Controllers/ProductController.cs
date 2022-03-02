using fotoTeca.Models.Product;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MercadoPago.Config;
using MercadoPago.Resource.Preference;

namespace fotoTeca.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductController : ControllerBase
    {
        private readonly ProductDAL _repository1;
        //private readonly MercadoPagoConfig.AccessToken = "PROD_ACCESS_TOKEN";

        public ProductController(ProductDAL repository1)
        {
            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));
        }
        [HttpPost("/StoreOrder", Name = "StoreOrder")]
        public async Task<object> Post1([FromBody] ProductOrderRequeride req)
        {
            return await _repository1.StoreOrder(req);
        }
        [HttpPost("/StoreOrderGiftCard", Name = "StoreOrderGiftCard")]
        public async Task<Preference> Post2([FromBody] ProductOrderRequeride2 req)
        {
            return await _repository1.StoreOrderGiftCard(req);
        }

        [HttpGet("/GetProduct")]
        public async Task<List<ProductResponse>> get1()
        {
            return await _repository1.GetProduct();
        }

        [HttpGet("/GetProduct/{idProduct}")]
        public async Task<List<ProductResponse>> get12(int idProduct)
        {
            return await _repository1.GetProduct(idProduct);
        }
        [HttpGet("/GetCategory")]
        public async Task<List<ProductResponseCategory>> get3()
        {
            return await _repository1.GetCategory();
        }

        [HttpPut("/UpdateStatusOrder", Name = "UpdateStatusOrder")]
        public async Task put3([FromBody] StatusRequride use)

        {
            await _repository1.UpdateStatusOrder(use);
        }

    }


}

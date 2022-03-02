using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NegronWebApi.Models.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegronWebApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PromotionController : ControllerBase
    {
        private readonly PromotionDAL _repository1;
        public PromotionController(PromotionDAL repository1)
        {

            _repository1 = repository1 ?? throw new ArgumentNullException(nameof(repository1));

        }

        [HttpPost("/storeOrUpdatePromotion", Name = "storeOrUpdatePromotion")]
        public async Task Post1([FromBody] PromotionRequeride apu)

        {
            await _repository1.storeOrUpdatePromotion(apu);
        }

        //GET-----------------------------------------------------------------------------------------------------------------------------------

        [HttpGet("/getPromotionType")]
        public async Task<List<PromotionTypeResponse>> get1()
        {
            return await _repository1.getPromotionType();
        }

        [HttpGet("/getPromotion")]
        public async Task<Promotion> get2()
        {
            return await _repository1.getPromotion();
        }
        [HttpGet("/getPromotion/{idPromotion}")]
        public async Task<Promotion> get22(int idPromotion)
        {
            return await _repository1.getPromotion(idPromotion);

        }
        [HttpGet("/getPromotionByCode/{PromotionCode}")]
        public async Task<Promotion> get222(string PromotionCode)
        {
            return await _repository1.getPromotionByCode(PromotionCode);

        }
        [HttpGet("/getStatusPromotion")]
        public async Task<List<StatusPromotionResponse>> get3()
        {
            return await _repository1.getStatusPromotion();

        }
        [HttpGet("/GetValidPromotions")]
        public async Task<List<ValidPromotionsResponse>> get4()
        {
            return await _repository1.GetValidPromotions();

        }
        //PUT--------------------------------------------------------------------------------------------------------------------------------------

        [HttpPut("/UpdateStatusPromotion", Name = "UpdateStatusPromotion")]
        public async Task put1([FromBody] StatusPromotionRequeride us)

        {
            await _repository1.UpdateStatusPromotion(us);
        }
    }
}
